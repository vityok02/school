using SchoolManagement.API.Features.Employees.Dtos;
using SchoolManagement.API.Features.Employees.Handlers;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Features.Employees;

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
            .Produces<EmployeeDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapPost("/", CreateEmployeeHandler.Handle)
            .WithSummary("Create employee")
            .Produces<EmployeeDto>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapPut("/{employeeId:int}", UpdateEmployeeHandler.Handle)
            .WithSummary("Update employee")
            .Produces<EmployeeDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status404NotFound);

        employeesGroup.MapDelete("/{employeeId:int}", DeleteEmployeeHandler.Handle)
            .WithSummary("Delete employee")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
