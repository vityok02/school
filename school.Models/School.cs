using System.Text;
using Newtonsoft.Json;

namespace school.Models;

public class School : BaseEntity
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public DateTime OpeningDate { get; set; }
    [JsonIgnore]
    public Employee? Director
    {
        get
        {
            foreach (Employee employee in Employees)
            {
                if (employee is Director director)
                {
                    return director;
                }
            }
            return null;
        }
    }
    public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    public ICollection<Student> Students { get; set; } = new HashSet<Student>();

    //private readonly List<Floor> _floors = new();
    //public IEnumerable<Floor> Floors => _floors;

    public ICollection<Floor> Floors { get; set; } = new HashSet<Floor>();

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

    public School()
    {
    }

    public School(string name, Address address, DateTime openingDate, ILogger logger)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
        SetLogger(logger);
    }

    public void AddFloor(Floor floor)
    {
        foreach(Floor f in Floors)
        {
            if (f.Number == floor.Number)
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

        Floors.Add(floor);
        _logger.LogSuccess($"{floor.Number} floor successfully added");
    }

    public void AddStudent(Student student)
    {
        foreach (Student s in Students)
        {
            Student stud = s;
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

        Students.Add(student);
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

        foreach (Employee emp in Employees)
        {
            if (emp.FirstName == employee.FirstName && emp.LastName == employee.LastName && emp.Age == employee.Age)
            {
                _logger.LogError("*This employee already exists*");
                _logger.LogInfo("---------------------------------------------");
                return;
            }
        }
        Employees.Add(employee);
        _logger.LogInfo("---------------------------------------------");
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine();
        sb.AppendLine($"==========Rooms=============+");

        foreach (Floor floor in Floors)
        {
            sb.AppendLine(floor.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Employees==========");
        foreach (Employee employee in Employees)
        {
            sb.AppendLine(employee.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Students===========");
        foreach (Student student in Students)
        {
            sb.AppendLine(student.ToString());
        }

        return sb.ToString();
    }
}