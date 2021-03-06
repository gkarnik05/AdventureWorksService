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
    public class SalesOrderDetailConfiguration : IEntityTypeConfiguration<SalesOrderDetail>
    {
        public void Configure(EntityTypeBuilder<SalesOrderDetail> entity)
        {
            entity.ToTable("SalesOrderDetail", "Sales");
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

            entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesOrderDetail_rowguid")
                .IsUnique();

            entity.Property(e => e.SalesOrderId).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

            entity.Property(e => e.SalesOrderDetailId)
                .HasComment("Primary key. One incremental unique number per product sold.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CarrierTrackingNumber).HasComment("Shipment tracking number supplied by the shipper.");

            entity.Property(e => e.LineTotal)
                .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.")
                .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

            entity.Property(e => e.ProductId).HasComment("Product sold to customer. Foreign key to Product.ProductID.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SpecialOfferId).HasComment("Promotional code. Foreign key to SpecialOffer.SpecialOfferID.");

            entity.Property(e => e.UnitPrice).HasComment("Selling price of a single product.");

            entity.Property(e => e.UnitPriceDiscount).HasComment("Discount amount.");

            entity.HasOne(d => d.SpecialOfferProduct)
                .WithMany(p => p.SalesOrderDetail)
                .HasForeignKey(d => new { d.SpecialOfferId, d.ProductId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderDetail_SpecialOfferProduct_SpecialOfferIDProductID");
        }
    }
}
