namespace school.Models;

public abstract class Employee : Person
{
    protected Employee(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }
    public abstract string Job { get; }

    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName} {Age} {Job}");
    }
}
