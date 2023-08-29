using SchoolManagement.API.Employees.Handlers;

namespace SchoolManagement.API.Employees;

public static class EmployeesEndpoints
{
    public static void Map(WebApplication app)
    {
        var employeesGroup = app.MapGroup("/schools/{schoolId}/employees");

        employeesGroup.MapGet("/", GetAllEmployeesHandler.Handle);
        employeesGroup.MapGet("/{employeeId}", GetEmployeeByIdHandler.Handle);
        employeesGroup.MapPost("/", CreateEmployeeHandler.Handle);
        employeesGroup.MapPut("/{employeeId}", UpdateEmployeeHandler.Handle);
        employeesGroup.MapDelete("/{employeeId}", DeleteEmployeeHandler.Handle);
    }
}
