using System.Text.Json.Serialization;
using school.Models;

namespace school;

public static class Context
{
    public static School? CurrentSchool { get; set; }

    private static List<School> _schools = new();
    public static IEnumerable<School> Schools => _schools;
    public static string? FileName { get; set; }

    public static void AddSchool(School school)
    {
        _schools.Add(school);
    }
}