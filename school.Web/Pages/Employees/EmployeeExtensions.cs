using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SchoolManagement.Models;

namespace SchoolManagement.Web.Pages.Employees;

public static class EmployeeExtensions
{
    public static EmployeeDto ToEmployeeDto(this AddEmployeeDto employee)
    {
        return new EmployeeDto(
            employee.FirstName,
            employee.LastName,
            employee.Age);
    }

    public static EmployeeDto ToEmployeeDto(this EditEmployeeDto employee)
    {
        return new EmployeeDto(
            employee.FirstName,
            employee.LastName,
            employee.Age);
    }

    public static EditEmployeeDto ToEditEmployeeDto(this Employee employee)
    {
        return new EditEmployeeDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            employee.Positions);
    }

    public static EmployeeItemDto ToEmployeeItemDto(this Employee employee)
    {
        var position = employee.Positions.FirstOrDefault()?.Name;
        position ??= "None";

        return new EmployeeItemDto(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            position);
    }

    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}
