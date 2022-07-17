namespace SchoolNamespace;

public abstract class Person
{    
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }

    protected Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;

    }
}
