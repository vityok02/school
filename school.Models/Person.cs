using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public abstract class Person : BaseEntity
{
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; private set; }
    [Required]
    public string LastName { get; private set; }

    [Required]
    [Range(18, 65)]
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