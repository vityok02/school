using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using school.Models;

namespace school.Data.Configurations;

public class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Directors");
        //builder.HasKey(t => t.Id);

        //builder.Property(t => t.Id)
        //    .ValueGeneratedOnAdd();

        //builder.HasOne(t => t.Employee)
        //    .WithOne();
    }
}
