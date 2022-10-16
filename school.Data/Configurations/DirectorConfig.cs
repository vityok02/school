using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using school.Models;

namespace school.Data.Configurations;

public class DirectorConfig : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.ToTable("Directors");
        //builder.HasKey(t => t.Id);

        //builder.Property(t => t.Id)
        //    .ValueGeneratedOnAdd();

        //builder.HasOne(t => t.Employee)
        //    .WithOne();
    }
}
