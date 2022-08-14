using System.Text.Json.Serialization;
using school;
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
                Director? director = employee as Director;
                if (director is not null)
                {
                    return director;
                }
            }
            return null;
        }
    }
    private readonly List<Employee> _employees = new();
    [JsonIgnore]
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

    public School(string name, Address address, string openingDate)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
    }

    [JsonConstructor]
    public School(string name,
        Address address,
        string openingDate,
        IEnumerable<Floor> floors,
        IEnumerable<Student> students)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
        _floors = floors.ToList();
        _students = students.ToList();
    }

    public void AddFloor(Floor floor)
    {
        for (int i = 0; i < _floors.Count; i++)
        {
            if (_floors[i].Number == floor.Number)
            {
                Console.WriteLine($"Floor {floor.Number} already exists");
                return;
            }
        }

        if (floor.Number < 0)
        {
            Console.WriteLine("*Floor`s number should be more than 0*");
            return;
        }

        if (floor.Number > 10)
        {
            Console.WriteLine("*Floor`s number shouldn`t be more than 10*");
            return;
        }

        _floors.Add(floor);
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
                Console.WriteLine("*This student already exists*");
                Console.WriteLine("---------------------------------------------");
                return;
            }
        }

        if (string.IsNullOrEmpty(student.FirstName))
        {
            Console.WriteLine("First name is not provided");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (string.IsNullOrEmpty(student.LastName))
        {
            Console.WriteLine("Last name is not provided");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (student.Age > 18)
        {
            Console.WriteLine("Employee shouldn`t be less then 18");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (student.Age < 5)
        {
            Console.WriteLine("Employee should be less then 65");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        _students.Add(student);
    }

    public void AddDirector(string firstName, string lastName, int age)
    {
        Console.WriteLine($"Adding director {firstName} {lastName} with age {age}");
        try
        {
            var director = new Director(firstName, lastName, age);
            if (director is Director && Director is not null)
            {
                Console.WriteLine("*The director already exists*");
                Console.WriteLine("---------------------------------------------");
                return;
            }

            AddEmployee(director);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void AddTeacher(string firstName, string lastName, int age)
    {
        Console.WriteLine($"Adding teacher {firstName} {lastName} with age {age}");
        try
        {
            AddEmployee(new Teacher(firstName, lastName, age));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddEmployee(Employee employee)
    {
        if (string.IsNullOrEmpty(employee.FirstName))
        {
            Console.WriteLine("First name is not provided");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (string.IsNullOrEmpty(employee.LastName))
        {
            Console.WriteLine("Last name is not provided");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (employee.Age < 18)
        {
            Console.WriteLine("Employee shouldn`t be less then 18");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        if (employee.Age > 65)
        {
            Console.WriteLine("Employee should be less then 65");
            Console.WriteLine("---------------------------------------------");
            return;
        }

        for (int i = 0; i < _employees.Count; i++)
        {
            Employee emp = _employees[i];
            if (emp.FirstName == employee.FirstName && emp.LastName == employee.LastName && emp.Age == employee.Age)
            {
                Console.WriteLine("*This employee already exists*");
                Console.WriteLine("---------------------------------------------");
                return;
            }
        }
        _employees.Add(employee);
        Console.WriteLine("---------------------------------------------");
    }

    public void Print()
    {
        Console.WriteLine();
        Console.WriteLine($"==========Rooms=============");
        foreach (Floor floor in _floors)
        {
            floor.Print();
        }

        Console.WriteLine();
        Console.WriteLine("==========Employees==========");
        foreach (Employee employee in _employees)
        {
            employee.Print();
        }

        Console.WriteLine();
        Console.WriteLine("==========Students===========");
        foreach (Student student in _students)
        {
            student.Print();
        }
    }
}
