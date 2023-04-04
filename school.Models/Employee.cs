﻿namespace SchoolManagement.Models;

public class Employee : Person
{
    public School School { get; set; } = null!;
    public int SchoolId { get; set; }
    public string Position 
    {
        get
        {
            string position = Positions.FirstOrDefault()?.Name!;
            if (position is null)
            {
                position = "None";
            }

            return position;
        }
    }

    public ICollection<Position> Positions { get; set; } = new HashSet<Position>();

    public Employee() { }

    public Employee(string firstName, string lastName, int age)
        : base(firstName, lastName, age)
    {
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Age}";
    }
}