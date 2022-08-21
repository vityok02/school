using System.Text.Json.Serialization;
using school.Models;

namespace school.Data;

public class Context
{
    [JsonIgnore]
    public School? CurrentSchool { get; set; }

    private List<School> _schools = new();
    public IEnumerable<School> Schools => _schools;

    public void AddSchool(School school)
    {
        _schools.Add(school);
    }

    public void SetSchools(IEnumerable<School> schools)
    {
        _schools = schools.ToList();
    }

    public Context()
    {
        _schools = new();
    }

    [JsonConstructor]
    public Context(IEnumerable<School> schools)
    {
        SetSchools(schools);
    }
}