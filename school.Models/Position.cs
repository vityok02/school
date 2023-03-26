namespace SchoolManagement.Models;

public class Position : BaseEntity
{
    public ICollection<School> Schools { get; set; } = new HashSet<School>();
    public string Name { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    public Position() { }

    public Position(string name)
    {
        Name = name;
    }
}
