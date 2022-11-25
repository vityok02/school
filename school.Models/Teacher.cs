namespace SchoolManagement.Models;

public class Teacher : Employee
{
    public override string Job => nameof(Teacher);

    private readonly List<string> _subjects;
    public ICollection<string> Subjects => _subjects;

    public Teacher(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
        _subjects = new List<string>();
    }
    public void AddSubject(string subject)
    {
        _subjects.Add(subject);
    }
}