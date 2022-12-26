namespace SchoolManagement.Models;

public abstract class Employee : Person
{
    public int SchoolId { get; set; }
    public School School { get; set; }
    public abstract string Job { get; }

    protected Employee()
    {

    }

    protected Employee(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age} {Job}";
    }
}