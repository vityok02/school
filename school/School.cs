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
            foreach (Employee employee in Employees)
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
        foreach (Floor floor in Floors)
        {
            floor.Print();
        }

        Console.WriteLine();
        Console.WriteLine("==========Employees==========");
        foreach (Employee employee in Employees)
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
    public void AddEmployee(Employee employee)
    {
        Console.WriteLine("Employee to add: ");
        employee.Print();

        if (employee is Director && Director is not null)
        {
            Console.WriteLine("Error");
            return;
        }
        _employees.Add(employee);
    }
}