namespace school.Models;

public class Director : Employee
{
    public Director(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public override string Job => "Director";
}