﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using school.Models;

namespace school.Data;
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

        //builder.Ignore(t => t.Employees);

        builder.HasMany(t => t.Employees)
            .WithOne(t => t.School)
            .HasForeignKey(t => t.SchoolId);

        //builder.Ignore(t => t.Directors);
        //builder.Ignore(t => t.Employees);

        //builder.HasOne(t => t.Director)
        //    .WithOne();

        //builder.HasMany(t => t.Floors)
        //    .WithOne(t => t.School);

        //builder.HasMany(t => t.Students)
        //    .WithOne();

        //builder.HasMany(t => t.Employees)
        //    .WithOne();
    }
}