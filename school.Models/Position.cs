namespace SchoolManagement.Models;

public class Position : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    public Position() { }

    public Position(string name)
    {
        Name = name;
    }
}
