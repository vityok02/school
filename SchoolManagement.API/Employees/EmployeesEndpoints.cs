using SchoolManagement.Models.Constants;
using SchoolManagement.API.Employees.Dtos;
using SchoolManagement.API.Employees.Handlers;
using SchoolManagement.API.Filters;

namespace SchoolManagement.API.Employees;

public static class EmployeesEndpoints
{
    public static void Map(WebApplication app)
    {
        var manageEmployeesGroup = app.MapGroup("/schools/{schoolId:int}/employees")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Manage employees group")
            .RequireAuthorization(builder =>
                builder.RequireClaim(ClaimNames.Permissions, Permissions.CanManageEmployees));

        var employeesInfoGroup = app.MapGroup("/schools/{schoolId:int}/employees")
            .AddEndpointFilter<SchoolIdExistsFilter>()
            .WithTags("Employees info Group")
            .WithOpenApi()
            .RequireAuthorization(Policies.CanViewInfo);

        employeesInfoGroup.MapGet("/", GetAllEmployeesHandler.Handle)
            .WithSummary("Get All Employees")
            .Produces<EmployeeDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        employeesInfoGroup.MapGet("/{employeeId:int}", GetEmployeeByIdHandler.Handle)
            .WithSummary("Get employee by id")
            .Produces<EmployeeDto>()
            .Produces(StatusCodes.Status404NotFound);

        manageEmployeesGroup.MapPost("/", CreateEmployeeHandler.Handle)
            .WithSummary("Create employee")
            .Produces<EmployeeDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageEmployeesGroup.MapPut("/{employeeId:int}", UpdateEmployeeHandler.Handle)
            .WithSummary("Update employee")
            .Produces<EmployeeDto>()
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status404NotFound);

        manageEmployeesGroup.MapDelete("/{employeeId:int}", DeleteEmployeeHandler.Handle)
            .WithSummary("Delete employee")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}
