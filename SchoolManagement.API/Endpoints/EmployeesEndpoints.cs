using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.API.Endpoints;

public static class EmployeesEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/schools/{id}/employees", async (IEmployeeRepository repository, int id) =>
            await repository.GetAllAsync(e => e.SchoolId == id));

        app.MapGet("/schools/{id}/employees/{employeeId}", async (IEmployeeRepository repository, int employeeId) =>
            await repository.GetAsync(employeeId));

        app.MapPost("/schools/{id}/employees", 
            async (
                IEmployeeRepository employeeRepository,
                IPositionRepository positionRepository,
                int id,
                string firstName,
                string lastName,
                int age,
                int[] positionIds) =>
            {
                //int[] positionIds = new int[1];
                //positionIds[0] = positionId;
                var positions = await positionRepository.GetEmployeePositions(positionIds);

                Employee employee = new(firstName, lastName, age)
                {
                    Positions = (ICollection<Position>)positions,
                    SchoolId = id
                };

                await employeeRepository.AddAsync(employee);
            });

        app.MapPut("/schools/{id}/employees/{employeeId}", 
            async (
                IEmployeeRepository employeeRepository,
                IPositionRepository positionRepository,
                Employee employeeData,
                int employeeId
                ) =>
                //int employeeId,
                //string? firstName,
                //string? lastName,
                //int? age,
                //int[]? positionIds) =>
            {
                var employee = await employeeRepository.GetAsync(employeeId)!;

                //employee!.UpdateInfo(
                //    firstName = firstName is null ? firstName! : employee.FirstName,
                //    lastName = lastName is null ? lastName! : employee.LastName,
                //    (int)(age = age is null ? age!.Value : employee.Age));


                //if (positionIds is not null)
                //{
                //    employee.Positions.Clear();
                //    foreach (var positionId in positionIds!)
                //    {
                //        var position = await positionRepository.GetAsync(positionId);
                //        employee!.Positions.Add(position!);
                //    }
                //}

                await employeeRepository.UpdateAsync(employee);
            });

        app.MapDelete("/schools/{id}/employees{employeeId}", async (IEmployeeRepository repository, int employeeId) =>
        {
            var employee = await repository.GetAsync(employeeId);

            await repository.DeleteAsync(employee!);
        });
    }
}
