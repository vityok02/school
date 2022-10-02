using Microsoft.EntityFrameworkCore;
using school.Models;
namespace school.Data;

public class AppDbContext : DbContext
{
    //DbSet<School> Schools => Set<School>();
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