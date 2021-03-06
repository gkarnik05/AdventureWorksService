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
    public class CountryRegionConfiguration : IEntityTypeConfiguration<CountryRegion>
    {
        public void Configure(EntityTypeBuilder<CountryRegion> entity)
        {
            entity.ToTable("CountryRegion", "Person");
            entity.HasKey(e => e.CountryRegionCode)
                    .HasName("PK_CountryRegion_CountryRegionCode");

            entity.HasComment("Lookup table containing the ISO standard codes for countries and regions.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_CountryRegion_Name")
                .IsUnique();

            entity.Property(e => e.CountryRegionCode).HasComment("ISO standard code for countries and regions.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Country or region name.");
        }
    }
}
