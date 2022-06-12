namespace School;

class Employee 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public void Print()
    {
        Console.WriteLine($"{LastName} {FirstName}");
    }
}
