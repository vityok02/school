namespace School;

class Director : Employee
{
    public override string Job => "Director";
    public void Print() => Console.WriteLine($"{FirstName} {LastName}");
}