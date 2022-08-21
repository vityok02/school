using JsonSubTypes;
using Newtonsoft.Json;

namespace school.Models;

[JsonConverter(typeof(JsonSubtypes), "Job")]
[JsonSubtypes.KnownSubType(typeof(Teacher), nameof(Teacher))]
[JsonSubtypes.KnownSubType(typeof(Director), nameof(Director))]
public abstract class Employee : Person
{
    protected Employee(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }
    public abstract string Job { get; }

    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName} {Age} {Job}");
    }
}
