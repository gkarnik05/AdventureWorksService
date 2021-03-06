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
    public class SalesTerritoryConfiguration : IEntityTypeConfiguration<SalesTerritory>
    {
        public void Configure(EntityTypeBuilder<SalesTerritory> entity)
        {
            entity.ToTable("SalesTerritory", "Sales");
            entity.HasKey(e => e.TerritoryId)
                    .HasName("PK_SalesTerritory_TerritoryID");

            entity.HasComment("Sales territory lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_SalesTerritory_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesTerritory_rowguid")
                .IsUnique();

            entity.Property(e => e.TerritoryId).HasComment("Primary key for SalesTerritory records.");

            entity.Property(e => e.CostLastYear)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Business costs in the territory the previous year.");

            entity.Property(e => e.CostYtd)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Business costs in the territory year to date.");

            entity.Property(e => e.CountryRegionCode).HasComment("ISO standard country or region code. Foreign key to CountryRegion.CountryRegionCode. ");

            entity.Property(e => e.Group).HasComment("Geographic area to which the sales territory belong.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Sales territory description");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesLastYear)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales in the territory the previous year.");

            entity.Property(e => e.SalesYtd)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales in the territory year to date.");

            entity.HasOne(d => d.CountryRegionCodeNavigation)
                .WithMany(p => p.SalesTerritory)
                .HasForeignKey(d => d.CountryRegionCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
