using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class DataSeeder
{
    public static async Task SeedData(AppDbContext dbContext)
    {
        await SeedSchools(dbContext);
        await SeedPositions(dbContext);
        await SeedFloors(dbContext);

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedSchools(AppDbContext dbContext)
    {
        if (dbContext.Schools.Any())
        {
            return;
        }

        var schools = new List<School>()
        {
            new School("Malynivka gymnasium",
                new Address("Ukraine", "Malynivka", "Kotsubinskogo street, 9", 22360),
                new DateTime(2019,05,09,9,15,0)),
            new School("Lityn Lyceum",
                new Address("Ukraine", "Lityn", "Soborna 42", 22300),
                new DateTime(2000, 01, 01, 1, 1, 0)),
            new School("VC NUFT",
                new Address("Ukraine", "Vinnitsa", "Pryvokzalna, 38", 21100),
                new DateTime(default))
        };

        await dbContext.Schools.AddRangeAsync(schools);
    }

    private static async Task SeedPositions(AppDbContext dbContext)
    {
        if (dbContext.Positions.Any())
        {
            return;
        }

        var positions = new List<Position>()
        {
            new Position("Director"),
            new Position("Teacher")
        };

        await dbContext.Positions.AddRangeAsync(positions);
    }

    private static async Task SeedFloors(AppDbContext dbContext)
    {
        if(dbContext.Floors.Any())
        {
            return;
        }

        var floor = new Floor(1);

        await dbContext.Floors.AddAsync(floor);
    }
}
