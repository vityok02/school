using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class DataSeeder
{
    public static async Task SeedData(AppDbContext dbContext)
    {
        await SeedSchools(dbContext);
        await SeedPositions(dbContext);

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedSchools(AppDbContext dbContext)
    {
        if (dbContext.Schools.Any())
        {
            return;
        }

        for (int i = 1; i < 100; i++)
        {
            var address = new Address($"Country {i.ToString()}", $"City {i.ToString()}", $"Street {i.ToString()}", i);
            var school = new School($"School {i.ToString()}", address, DateTime.Now);
            await dbContext.Schools.AddAsync(school);
        }

        //var schools = new List<School>()
        //{
        //    new School("Malynivka gymnasium",
        //        new Address("Ukraine", "Malynivka", "Kotsubinskogo street, 9", 22360),
        //        new DateTime(2019,05,09,9,15,0)),
        //    new School("Lityn Lyceum",
        //        new Address("Ukraine", "Lityn", "Soborna 42", 22300),
        //        new DateTime(2000, 01, 01, 1, 1, 0)),
        //    new School("VC NUFT",
        //        new Address("Ukraine", "Vinnitsa", "Pryvokzalna, 38", 21100),
        //        new DateTime(default))
        //};

        //await dbContext.Schools.AddRangeAsync(schools);
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
}
