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
    public class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
    {
        public void Configure(EntityTypeBuilder<StateProvince> entity)
        {
            entity.ToTable("StateProvince", "Person");
            entity.HasComment("State and province lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_StateProvince_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_StateProvince_rowguid")
                .IsUnique();

            entity.HasIndex(e => new { e.StateProvinceCode, e.CountryRegionCode })
                .HasName("AK_StateProvince_StateProvinceCode_CountryRegionCode")
                .IsUnique();

            entity.Property(e => e.StateProvinceId).HasComment("Primary key for StateProvince records.");

            entity.Property(e => e.CountryRegionCode).HasComment("ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ");

            entity.Property(e => e.IsOnlyStateProvinceFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("State or province description.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StateProvinceCode)
                .IsFixedLength()
                .HasComment("ISO standard state or province code.");

            entity.Property(e => e.TerritoryId).HasComment("ID of the territory in which the state or province is located. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.HasOne(d => d.CountryRegionCodeNavigation)
                .WithMany(p => p.StateProvince)
                .HasForeignKey(d => d.CountryRegionCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.StateProvince)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
