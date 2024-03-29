﻿using System.Text;

namespace SchoolManagement.Models;

public class School : BaseEntity
{
    public string Name { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public int AddressId { get; set; }
    public DateTime OpeningDate { get; set; }
    public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    public ICollection<Position> Positions { get; set; } = new HashSet<Position>();
    public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    public ICollection<Floor> Floors { get; set; } = new HashSet<Floor>();
    public School()
    {
    }

    public School(string name, Address address, DateTime openingDate)
    {
        Name = name;
        Address = address;
        OpeningDate = openingDate;
    }

    public (bool Valid, string? Error) AddFloor(Floor floor)
    {
        if (Floors.Any(f => f.Number == floor.Number))
        {
            return (false, $"Floor {floor.Number} already exists");
        }

        Floors.Add(floor);
        return (true, null);
    }

    public (bool Valid, string? Error) AddStudent(Student student)
    {
        foreach (Student s in Students)
        {
            Student stud = s;
            if (stud.FirstName == student.FirstName
                && stud.LastName == student.LastName
                && stud.Age == student.Age)
            {
                return (false, "*This student already exists*");
            }
        }

        if (string.IsNullOrEmpty(student.FirstName) || string.IsNullOrEmpty(student.LastName))
        {
            return (false, "First name or last name are not provided");
        }

        if (student.Age < 5 || student.Age > 18)
        {
            return (false, "Student shouldn`t be under 5 or over 18");
        }

        Students.Add(student);
        return(true, null);
    }

    public (bool Valid, string? Error) AddEmployee(Employee employee)
    {

        if (string.IsNullOrEmpty(employee.FirstName) || string.IsNullOrEmpty(employee.LastName))
        {
            return (false, "First name or last name are not provided");
        }

        if (employee.Age < 18 || employee.Age > 65)
        {
            return (false, "Employee shouldn`t be under 18 or over 65");
        }

        //if (Employees.Any(emp => emp.FirstName == employee.FirstName &&
        //    emp.LastName == employee.LastName &&
        //    emp.Age == employee.Age))
        //{
        //    return (false, "This employee already exists");
        //}

        //Employees.Add(employee);
        return(true, null);
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine();
        sb.AppendLine($"==========Rooms==============");

        foreach (Floor floor in Floors)
        {
            sb.AppendLine(floor.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Employees==========");
        //foreach (Employee employee in Employees)
        {
            //sb.AppendLine(employee.ToString());
        }

        sb.AppendLine();
        sb.AppendLine("==========Students===========");
        foreach (Student student in Students)
        {
            sb.AppendLine(student.ToString());
        }

        return sb.ToString();
    }
}