using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Models;

namespace SchoolManagement.Data;
public class SchoolConfig : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.ToTable("Schools");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.OpeningDate)
            .HasColumnType("date");

        builder.HasOne(t => t.Address)
            .WithMany();

        //builder.HasMany(t => t.Employees)
        //    .WithOne(t => t.School)
        //    .HasForeignKey(t => t.SchoolId);
    }
}