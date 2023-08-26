using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;
using SchoolManagement.API.Employees.Dtos;

namespace SchoolManagement.API.Employees;

public static class EmployeesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/employees", async (IEmployeeRepository repository, int id) =>
        {
            var schools = await repository.GetAllAsync(e => e.SchoolId == id);

            return schools.Select(s => s.ToEmployeeDto());
        });

        app.MapGet("/schools/{id}/employees/{employeeId}",
            async (IEmployeeRepository repository, int employeeId, int id) =>
            {
                var employee = await repository.GetEmployeeWithPositionsAsync(employeeId);

                if (employee is null || employee.SchoolId != id)
                {
                    return Results.NotFound("No such employee found");
                }

                var employeeDto = employee.ToEmployeeDto();
                return Results.Json(employeeDto);
            });

        app.MapPost("/schools/{id}/employees",
            async (
                IEmployeeRepository employeeRepository,
                IPositionRepository positionRepository,
                EmployeeAddDto employeeDto,
                int id) =>
            {
                var employee = new Employee(employeeDto.FirstName!, employeeDto.LastName!, employeeDto.Age)
                {
                    Positions = (ICollection<Position>)await positionRepository
                        .GetEmployeePositions(employeeDto.PositionIds),
                    SchoolId = id
                };

                await employeeRepository.AddAsync(employee);
            });

        app.MapPut("/schools/{id}/employees/{employeeId}",
            async (
                IEmployeeRepository employeeRepository,
                IPositionRepository positionRepository,
                EmployeeEditDto employeeDto,
                int employeeId,
                int id) =>
            {
                var employee = await employeeRepository.GetAsync(employeeId);

                if (employee is null || employee.SchoolId != employeeId)
                {
                    return Results.NotFound("No such employee found");
                }

                employee!.UpdateInfo(employeeDto.FirstName, employeeDto.LastName, employeeDto.Age);

                await employeeRepository.UpdateAsync(employee);

                return Results.Json(employee);
            });

        app.MapDelete("/schools/{id}/employees/{employeeId}",
            async (IEmployeeRepository repository, int employeeId, int id) =>
            {
                var employee = await repository.GetAsync(employeeId);

                if (employee is null || employee.SchoolId != id)
                {
                    return Results.NotFound("No such employee found");
                }

                await repository.DeleteAsync(employee!);

                return Results.Ok();
            });
    }
}
