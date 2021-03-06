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
    public class CurrencyRateConfiguration : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> entity)
        {
            entity.ToTable("CurrencyRate", "Sales");
            entity.HasComment("Currency exchange rates.");

            entity.HasIndex(e => new { e.CurrencyRateDate, e.FromCurrencyCode, e.ToCurrencyCode })
                .HasName("AK_CurrencyRate_CurrencyRateDate_FromCurrencyCode_ToCurrencyCode")
                .IsUnique();

            entity.Property(e => e.CurrencyRateId).HasComment("Primary key for CurrencyRate records.");

            entity.Property(e => e.AverageRate).HasComment("Average exchange rate for the day.");

            entity.Property(e => e.CurrencyRateDate).HasComment("Date and time the exchange rate was obtained.");

            entity.Property(e => e.EndOfDayRate).HasComment("Final exchange rate for the day.");

            entity.Property(e => e.FromCurrencyCode)
                .IsFixedLength()
                .HasComment("Exchange rate was converted from this currency code.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ToCurrencyCode)
                .IsFixedLength()
                .HasComment("Exchange rate was converted to this currency code.");

            entity.HasOne(d => d.FromCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateFromCurrencyCodeNavigation)
                .HasForeignKey(d => d.FromCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ToCurrencyCodeNavigation)
                .WithMany(p => p.CurrencyRateToCurrencyCodeNavigation)
                .HasForeignKey(d => d.ToCurrencyCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
