﻿namespace SchoolManagement.API.Employees.Dtos;

public interface IEmployeeDto
{
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }
}
