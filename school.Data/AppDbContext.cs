using Microsoft.EntityFrameworkCore;
using school.Models;
namespace school.Data;

public class AppDbContext : DbContext
{
    public School CurrentSchool;
    public DbSet<Address> Address { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    //public DbSet<Teacher> Teachers { get; set; }
    //public DbSet<Director> Directors { get; set; }
    //public DbSet<Student> Students { get; set; }
    private List<School> _schools = new();

    public void SetSchools(IEnumerable<School> schools)
    {
        _schools = schools.ToList();
    }

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
}