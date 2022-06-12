namespace School;

class Teacher : Employee
{
    public override string Job => "Teacher";
    private readonly List<string> _subjects;
    public ICollection<string> Subjects => _subjects;
    public Teacher()
    {
        _subjects = new List<string>();
    }
    public void AddSubject(string subject)
    {
        _subjects.Add(subject);
    }
}