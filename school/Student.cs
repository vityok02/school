namespace SchoolNamespace;

public class Student : Person
{
    public int Class;
    public Student(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }
    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName} {Age}");
    }
}

