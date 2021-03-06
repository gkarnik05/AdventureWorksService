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
    public class CountryRegionCurrencyConfiguration : IEntityTypeConfiguration<CountryRegionCurrency>
    {
        public void Configure(EntityTypeBuilder<CountryRegionCurrency> entity)
        {
            entity.ToTable("CountryRegionCurrency", "Sales");
            entity.HasKey(e => new { e.CountryRegionCode, e.CurrencyCode })
                    .HasName("PK_CountryRegionCurrency_CountryRegionCode_CurrencyCode");

            entity.HasComment("Cross-reference table mapping ISO currency codes to a country or region.");

            entity.HasIndex(e => e.CurrencyCode);

            entity.Property(e => e.CountryRegionCode).HasComment("ISO code for countries and regions. Foreign key to CountryRegion.CountryRegionCode.");

            entity.Property(e => e.CurrencyCode)
                .IsFixedLength()
                .HasComment("ISO standard currency code. Foreign key to Currency.CurrencyCode.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.CountryRegionCodeNavigation)
                .WithMany(p => p.CountryRegionCurrency)
                .HasForeignKey(d => d.CountryRegionCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CurrencyCodeNavigation)
                .WithMany(p => p.CountryRegionCurrency)
                .HasForeignKey(d => d.CurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
