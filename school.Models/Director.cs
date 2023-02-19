namespace SchoolManagement.Models;

public class Director : Employee
{
    public int SchoolId { get; set; }
    public School School { get; set; }

    public Director(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public Director()
    {
    }

    public override string Job => nameof(Director);
}