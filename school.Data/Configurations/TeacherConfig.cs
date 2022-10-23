using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using school.Models;
using System.Security.Cryptography.X509Certificates;

namespace school.Data.Configurations;

public class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.Property(t => t.FirstName)
            .IsRequired();
        builder.Property(t => t.LastName)
            .IsRequired();
        //builder.HasKey(t => t.Id);

        //builder.Property(t => t.Id)
        //    .ValueGeneratedOnAdd();

        //builder.HasOne(t => t.Employee)
        //    .WithOne();
    }
}
