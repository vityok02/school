namespace school.Models;

public class Student : Person
{
    public string Group { get; }
    public Student(string firstName, string lastName, int age, string group)
        : base(firstName, lastName, age)
    {
        Group = group;
    }
    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName} {Age} group: {Group}");
    }
}

