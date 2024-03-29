﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Employees.Handlers;

public static class UpdateEmployeeHandler
{
    public static async Task<IResult> Handle(
        IValidator<IEmployeeDto> validator,
        IEmployeeRepository employeeRepository,
        [FromRoute] int schoolId,
        [FromRoute] int employeeId,
        [FromBody] EmployeeUpdateDto employeeDto)
    {
        var validationResult = await validator.ValidateAsync(employeeDto);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var employees = await employeeRepository.GetAllAsync(e => e.SchoolId == schoolId && e.Id != employeeDto.Id);

        if (employees.Any(
            e => e.FirstName == employeeDto.FirstName
            && e.LastName == employeeDto.LastName
            && e.Age == employeeDto.Age))
        {
            return Results.BadRequest("Such employee already exists");
        }

        var employee = await employeeRepository.GetAsync(employeeId);

        if (employee is null || employee.SchoolId != schoolId)
        {
            return Results.NotFound("No such employee found");
        }

        employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age);

        await employeeRepository.UpdateAsync(employee);

        var updatedEmployeeDto = employee.ToEmployeeDto();
        return Results.Ok(updatedEmployeeDto);
    }
}
