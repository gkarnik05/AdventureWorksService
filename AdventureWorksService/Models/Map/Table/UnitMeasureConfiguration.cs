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
    public class UnitMeasureConfiguration : IEntityTypeConfiguration<UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitMeasure> entity)
        {
            entity.HasKey(e => e.UnitMeasureCode)
                    .HasName("PK_UnitMeasure_UnitMeasureCode");

            entity.HasComment("Unit of measure lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_UnitMeasure_Name")
                .IsUnique();

            entity.Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .HasComment("Primary key.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Unit of measure description.");
        }
    }
}
