namespace school.Models;

public class Director : Employee
{
    public Director(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public Director()
    {

    }

    //public Employee Employee { get; set; }

    public override string Job => nameof(Director);
}