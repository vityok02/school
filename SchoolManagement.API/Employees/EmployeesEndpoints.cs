using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.API.Employees.Handlers;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Employees;

public static class EmployeesEndpoints
{
    public static void Map(WebApplication app)
    {
        var employeesGroup = app.MapGroup("/schools/{schoolId:int}/employees")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Employees Group")
            .WithOpenApi();

        employeesGroup.MapGet("/", GetAllEmployeesHandler.Handle)
            .WithSummary("Get All Employees")
            .Produces<EmployeeDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapGet("/{employeeId:int}", GetEmployeeByIdHandler.Handle)
            .WithSummary("Get employee by id")
            .Produces<EmployeeDto>()
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapPost("/", CreateEmployeeHandler.Handle)
            .WithSummary("Create employee")
            .Produces<EmployeeDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapPut("/{employeeId:int}", UpdateEmployeeHandler.Handle)
            .WithSummary("Update employee")
            .Produces<EmployeeDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapDelete("/{employeeId:int}", DeleteEmployeeHandler.Handle)
            .WithSummary("Delete employee")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
