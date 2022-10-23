using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using school.Models;

namespace school.Data.Configurations;

public class DirectorConfig : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.ToTable("Directors");

        builder.HasBaseType(typeof(Employee));
        //builder.HasKey(t => t.Id);

        //builder.Property(t => t.Id)
        //    .ValueGeneratedOnAdd();

        //builder.HasOne(t => t.School)
        //    .WithOne(t => t.Director)
        //    .HasForeignKey<Director>(t => t.Id);
    }
}
