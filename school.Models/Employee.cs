using JsonSubTypes;
using Newtonsoft.Json;
using System.Reflection.PortableExecutable;

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
    protected Employee()
    {

    }
    public int SchoolId { get; set; }
    public School School { get; set; }
    public abstract string Job { get; }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age} {Job}";
    }
}
