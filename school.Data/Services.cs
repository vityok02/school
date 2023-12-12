using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Models.Interfaces;

namespace SchoolManagement.Data;

public static class Services
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("localdb");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<ISchoolRepository, SchoolRepository>();
        services.AddScoped<IFloorRepository, FloorRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        return services;
    }
}
