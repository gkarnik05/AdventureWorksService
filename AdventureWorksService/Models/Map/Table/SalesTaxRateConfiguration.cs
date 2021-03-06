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
    public class SalesTaxRateConfiguration : IEntityTypeConfiguration<SalesTaxRate>
    {
        public void Configure(EntityTypeBuilder<SalesTaxRate> entity)
        {
            entity.ToTable("SalesTaxRate", "Sales");
            entity.HasComment("Tax rate lookup table.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesTaxRate_rowguid")
                .IsUnique();

            entity.HasIndex(e => new { e.StateProvinceId, e.TaxType })
                .HasName("AK_SalesTaxRate_StateProvinceID_TaxType")
                .IsUnique();

            entity.Property(e => e.SalesTaxRateId).HasComment("Primary key for SalesTaxRate records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Tax rate description.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StateProvinceId).HasComment("State, province, or country/region the sales tax applies to.");

            entity.Property(e => e.TaxRate)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax rate amount.");

            entity.Property(e => e.TaxType).HasComment("1 = Tax applied to retail transactions, 2 = Tax applied to wholesale transactions, 3 = Tax applied to all sales (retail and wholesale) transactions.");

            entity.HasOne(d => d.StateProvince)
                .WithMany(p => p.SalesTaxRate)
                .HasForeignKey(d => d.StateProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
