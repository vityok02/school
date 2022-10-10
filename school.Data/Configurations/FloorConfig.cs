using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using school.Models;

namespace school.Data
{
    public class FloorConfig : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.ToTable("Floors");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(t => t.School)
                .WithMany(t => t.Floors);

            builder.HasMany(t => t.Rooms)
                .WithOne(t => t.Floor);
        }
    }
}
