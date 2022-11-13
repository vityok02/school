namespace school.Models;

public class Student : Person
{
    public string Group { get; }
    public School School { get; set; }

    public Student() 
    {

    }
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

