using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Models;

namespace SchoolManagement.Data
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Age)
                .IsRequired();

            builder.Property(t => t.Group)
                .IsRequired();

            builder.HasOne(t => t.School)
                .WithMany(t => t.Students);
        }
    }
}
