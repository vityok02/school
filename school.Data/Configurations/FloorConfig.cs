using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Models;

namespace SchoolManagement.Data;
public class FloorConfig : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable("Floors");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => new { t.Number, t.SchoolId })
            .IsUnique();

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(t => t.School)
            .WithMany(t => t.Floors)
            .HasForeignKey(t => t.SchoolId);
    }
}