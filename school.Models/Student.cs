namespace SchoolManagement.Models;

public class Student : Person
{
    public int SchoolId { get; set; }
    public string Group { get; private set; }
    public School School { get; set; }

    public Student() 
    {
    }

    public Student(string firstName, string lastName, int age, string group)
        : base(firstName, lastName, age)
    {
        Group = group;
    }

    public void UpdateInfo(string firstName, string lastName, int age, string group)
    {
        UpdateInfo(firstName, lastName, age);
        Group = group;
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age} group: {Group}";
    }
}