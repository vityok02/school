namespace SchoolManagement.Models;

public class Employee : Person
{
    public School School { get; set; } = null!;
    public int SchoolId { get; set; }
    public string Position => Positions.FirstOrDefault()!.Name;
    public ICollection<Position> Positions { get; set; } = new HashSet<Position>();

    public Employee() { }

    public Employee(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age}";
    }
}