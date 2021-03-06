using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdventureWorksService.WebApi.Data;

namespace AdventureWorksService.WebApi.Models.Map
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> entity)
        {
            entity.ToTable("Shift", "HumanResources");
            entity.HasComment("Work shift lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Shift_Name")
                .IsUnique();

            entity.HasIndex(e => new { e.StartTime, e.EndTime })
                .HasName("AK_Shift_StartTime_EndTime")
                .IsUnique();

            entity.Property(e => e.ShiftId)
                .HasComment("Primary key for Shift records.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EndTime).HasComment("Shift end time.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Shift description.");

            entity.Property(e => e.StartTime).HasComment("Shift start time.");
        }
    }
}
