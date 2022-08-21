using school.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace school.Data;

public class SchoolRepository
{
    private readonly Context _ctx;
    private readonly string _fileName;

    public SchoolRepository(Context ctx, string fileName)
    {
        _ctx = ctx;
        _fileName = fileName;

        LoadData();
    }

    public void SaveContext()
    {
        var json = JsonSerializer.Serialize(_ctx, new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        });
        File.WriteAllText(_fileName, json);
    }

    private void LoadData()
    {
        if (!File.Exists(_fileName))
        {
            return;
        }

        var content = File.ReadAllText(_fileName);
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        var ctx = JsonSerializer.Deserialize<Context>(content);
        if(ctx is null)
        {
            return;
        }

        _ctx.SetSchools(ctx.Schools);
    }

    public IEnumerable<School> GetSchools()
    {
        return _ctx.Schools;
    }

    public School? GetSchool(string name)
    {
        return _ctx.Schools.Where(s => s.Name == name).SingleOrDefault();
    }

    public School? GetCurrentSchool()
    {
        return _ctx.CurrentSchool;
    }

    public void SetCurrentSchool(School? school)
    {
        _ctx.CurrentSchool = school;
    }

    public void AddSchool(School school)
    {
        _ctx.AddSchool(school);
        SetCurrentSchool(school);
        SaveContext();
    }

    public void AddFloorToCurrentSchool(Floor floor)
    {
        _ctx.CurrentSchool?.AddFloor(floor);
        SaveContext();
    }
    public void AddRoomToCurrentSchool(Room room, Floor floor)
    {
        floor.AddRoom(room);
        SaveContext();
    }

    public void AddEmployeeToCurrentSchool(Employee employee)
    {
        _ctx.CurrentSchool?.AddEmployee(employee);
        SaveContext();
    }

    public void AddStudentToCurrentSchool(Student student)
    {
        _ctx.CurrentSchool?.AddStudent(student);
        SaveContext();
    }

    public Floor? GetFloor(int floorNumber)
    {
        return _ctx.CurrentSchool?.Floors.Where(f => f.Number == floorNumber).FirstOrDefault();
    }
}