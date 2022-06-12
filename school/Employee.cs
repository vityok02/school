namespace School;

abstract class Employee : Person
{
    public abstract string Job { get; }

    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName} {Age} {Job}");
    }
}
