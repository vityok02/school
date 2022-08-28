namespace school.Models;

public class Student : Person
{
    private ILogger _logger;

    public string Group { get; }
    public Student(string firstName, string lastName, int age, string group)
        : base(firstName, lastName, age)
    {
        Group = group;
    }
    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age} group: {Group}";
    }
}

