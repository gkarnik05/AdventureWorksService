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
    public class ProductVendorConfiguration : IEntityTypeConfiguration<ProductVendor>
    {
        public void Configure(EntityTypeBuilder<ProductVendor> entity)
        {
            entity.ToTable("ProductVendor", "Purchasing");
            entity.HasKey(e => new { e.ProductId, e.BusinessEntityId })
                    .HasName("PK_ProductVendor_ProductID_BusinessEntityID");

            entity.HasComment("Cross-reference table mapping vendors with the products they supply.");

            entity.HasIndex(e => e.BusinessEntityId);

            entity.HasIndex(e => e.UnitMeasureCode);

            entity.Property(e => e.ProductId).HasComment("Primary key. Foreign key to Product.ProductID.");

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key. Foreign key to Vendor.BusinessEntityID.");

            entity.Property(e => e.AverageLeadTime).HasComment("The average span of time (in days) between placing an order with the vendor and receiving the purchased product.");

            entity.Property(e => e.LastReceiptCost).HasComment("The selling price when last purchased.");

            entity.Property(e => e.LastReceiptDate).HasComment("Date the product was last received by the vendor.");

            entity.Property(e => e.MaxOrderQty).HasComment("The minimum quantity that should be ordered.");

            entity.Property(e => e.MinOrderQty).HasComment("The maximum quantity that should be ordered.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OnOrderQty).HasComment("The quantity currently on order.");

            entity.Property(e => e.StandardPrice).HasComment("The vendor's usual selling price.");

            entity.Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .HasComment("The product's unit of measure.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.ProductVendor)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductVendor)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UnitMeasureCodeNavigation)
                .WithMany(p => p.ProductVendor)
                .HasForeignKey(d => d.UnitMeasureCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
