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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.ToTable("Location", "Production");
            entity.HasComment("Product inventory and manufacturing locations.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Location_Name")
                .IsUnique();

            entity.Property(e => e.LocationId).HasComment("Primary key for Location records.");

            entity.Property(e => e.Availability)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Work capacity (in hours) of the manufacturing location.");

            entity.Property(e => e.CostRate)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Standard hourly cost of the manufacturing location.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Location description.");
        }
    }
}
