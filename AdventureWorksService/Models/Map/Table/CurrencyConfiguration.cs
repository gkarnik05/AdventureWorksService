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
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> entity)
        {
            entity.ToTable("Currency", "Sales");
            entity.HasKey(e => e.CurrencyCode)
                    .HasName("PK_Currency_CurrencyCode");

            entity.HasComment("Lookup table containing standard ISO currencies.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Currency_Name")
                .IsUnique();

            entity.Property(e => e.CurrencyCode)
                .IsFixedLength()
                .HasComment("The ISO code for the Currency.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Currency name.");
        }
    }
}
