namespace school.Models;

public abstract class Person : BaseEntity
{
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }
    protected Person()
    {

    }

    protected Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;

    }
}
