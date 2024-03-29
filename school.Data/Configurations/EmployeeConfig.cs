﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Models;

namespace SchoolManagement.Data;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
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

        builder.HasOne(t => t.School)
            .WithMany(t => t.Employees);

        builder.HasMany(t => t.Positions)
        .WithMany(t => t.Employees);
    }
}
