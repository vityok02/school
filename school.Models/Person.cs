using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public abstract class Person : BaseEntity
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public int Age { get; private set; }

    protected Person()
    {
    }

    protected Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public void UpdateInfo(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}
