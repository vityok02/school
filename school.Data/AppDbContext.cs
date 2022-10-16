using Microsoft.EntityFrameworkCore;
using school.Models;
namespace school.Data;

public class AppDbContext : DbContext
{
    public School CurrentSchool { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Student> Students { get; set; }

    private readonly string _connectionString;
    public AppDbContext(string connectionString)
    {
        _connectionString = connectionString;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SchoolConfig());
        modelBuilder.ApplyConfiguration(new FloorConfig());
        modelBuilder.ApplyConfiguration(new RoomConfig());
        modelBuilder.ApplyConfiguration(new StudentConfig());
        modelBuilder.ApplyConfiguration(new EmployeeConfig());
    }
}