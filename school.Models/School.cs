using System.Text;
using Newtonsoft.Json;

namespace school.Models;

public class School
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public string OpeningDate { get; set; }
    [JsonIgnore]
    public Employee? Director
    {
        get
        {
            foreach (Employee employee in _employees)
            {
                if (employee is Director director)
                {
                    return director;
                }
            }
            return null;
        }
    }
    private readonly List<Employee> _employees = new();
    public IEnumerable<Employee> Employees => _employees;

    private readonly List<Student> _students = new();
    public IEnumerable<Student> Students => _students;

    private readonly List<Floor> _floors = new();
    public IEnumerable<Floor> Floors => _floors;

    [JsonIgnore]
    public IEnumerable<Room> Rooms
    {
        get
        {
            List<Room> allRooms = new List<Room>();
            foreach (Floor floor in Floors)
            {
                allRooms.AddRange(floor.Rooms);
            }
            return allRooms;
        }
    }

    private ILogger _logger;
    public void SetLogger(ILogger logger)
    {
        _logger = logger;
    }

    public School(string name, Address address, string openingDate, ILogger logger)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
        SetLogger(logger);
    }

    [JsonConstructor]
    public School(string name,
        Address address,
        string openingDate,
        IEnumerable<Floor> floors,
        IEnumerable<Employee> employees,
        IEnumerable<Student> students)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
        _floors = floors.ToList();
        _employees = employees.ToList();
        _students = students.ToList();
    }

    public void AddFloor(Floor floor)
    {
        for (int i = 0; i < _floors.Count; i++)
        {
            if (_floors[i].Number == floor.Number)
            {
                _logger.LogError($"Floor {floor.Number} already exists");
                return;
            }
        }

        if (floor.Number < 0)
        {
            _logger.LogError("*Floor`s number should be more than 0*");
            return;
        }

        if (floor.Number > 10)
        {
            _logger.LogError("*Floor`s number shouldn`t be more than 10*");
            return;
        }

        _floors.Add(floor);
        _logger.LogSuccess($"{floor.Number} floor successfully added");
    }

    public void AddStudent(Student student)
    {
        for (int i = 0; i < _students.Count; i++)
        {
            Student stud = _students[i];
            if (stud.FirstName == student.FirstName &&
                stud.LastName == student.LastName &&
                stud.Age == student.Age)
            {
                _logger.LogError("*This student already exists*");
                _logger.LogInfo("---------------------------------------------");
                return;
            }
        }

        if (string.IsNullOrEmpty(student.FirstName))
        {
            _logger.LogError("First name is not provided");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (string.IsNullOrEmpty(student.LastName))
        {
            _logger.LogError("Last name is not provided");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (student.Age > 18)
        {
            _logger.LogError("Employee shouldn`t be less then 18");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (student.Age < 5)
        {
            _logger.LogError("Employee should be less then 65");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        _students.Add(student);
    }

    public void AddEmployee(Employee employee)
    {
        _logger.LogSuccess($"Employee {employee.Job} {employee.FirstName} {employee.LastName} successfully added");

        if (employee is Director && Director is not null)
        {
            _logger.LogError("The director already exists*");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (string.IsNullOrEmpty(employee.FirstName))
        {
            _logger.LogError("First name is not provided");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (string.IsNullOrEmpty(employee.LastName))
        {
            _logger.LogError("Last name is not provided");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (employee.Age < 18)
        {
            _logger.LogError("Employee shouldn`t be less then 18");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        if (employee.Age > 65)
        {
            _logger.LogError("Employee should be less then 65");
            _logger.LogInfo("---------------------------------------------");
            return;
        }

        for (int i = 0; i < _employees.Count; i++)
        {
            Employee emp = _employees[i];
            if (emp.FirstName == employee.FirstName && emp.LastName == employee.LastName && emp.Age == employee.Age)
            {
                _logger.LogError("*This employee already exists*");
                _logger.LogInfo("---------------------------------------------");
                return;
            }
        }
        _employees.Add(employee);
        _logger.LogInfo("---------------------------------------------");
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine();
        sb.AppendLine($"==========Rooms=============+");

        foreach (Floor floor in _floors)
        {
            sb.AppendLine(floor.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Employees==========");
        foreach (Employee employee in _employees)
        {
            sb.AppendLine(employee.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Students===========");
        foreach (Student student in _students)
        {
            sb.AppendLine(student.ToString());
        }

        return sb.ToString();
    }
}