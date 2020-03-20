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
    public class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> entity)
        {
            entity.ToTable("Vendor", "Purchasing");
            entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Vendor_BusinessEntityID");

            entity.HasComment("Companies from whom Adventure Works Cycles purchases parts or other goods.");

            entity.HasIndex(e => e.AccountNumber)
                .HasName("AK_Vendor_AccountNumber")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Primary key for Vendor records.  Foreign key to BusinessEntity.BusinessEntityID")
                .ValueGeneratedNever();

            entity.Property(e => e.AccountNumber).HasComment("Vendor account (identification) number.");

            entity.Property(e => e.ActiveFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Vendor no longer used. 1 = Vendor is actively used.");

            entity.Property(e => e.CreditRating).HasComment("1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Company name.");

            entity.Property(e => e.PreferredVendorStatus)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.");

            entity.Property(e => e.PurchasingWebServiceUrl).HasComment("Vendor URL.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Vendor)
                .HasForeignKey<Vendor>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
