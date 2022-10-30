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
    }
}
