using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data.Configurations;
using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class AppDbContext : DbContext
{
    public School CurrentSchool { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<School> Schools { get; set; } = null!;
    public DbSet<Floor> Floors { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SchoolConfig());
        modelBuilder.ApplyConfiguration(new FloorConfig());
        modelBuilder.ApplyConfiguration(new RoomConfig());
        modelBuilder.ApplyConfiguration(new StudentConfig());
        modelBuilder.ApplyConfiguration(new EmployeeConfig());
        modelBuilder.ApplyConfiguration(new PositionConfig());
    }
}