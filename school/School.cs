namespace School;

class School
{
    public Address Address { get; set; }
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
    public DateOnly OpeningDate { get; set; }
    public string Name { get; set; }
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
    public void Print()
    {
        Console.WriteLine();
        Console.WriteLine($"==========Rooms==========");
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
    }
    private readonly List<Floor> _floors;
    public IEnumerable<Floor> Floors => _floors;
    public School()
    {
        _floors = new List<Floor>();
        _employees = new List<Employee>();
    }
    public void AddFloor(Floor floor)
    {
        _floors.Add(floor);
    }
    private readonly List<Employee> _employees;
    public IEnumerable<Employee> Employees => _employees;

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
}