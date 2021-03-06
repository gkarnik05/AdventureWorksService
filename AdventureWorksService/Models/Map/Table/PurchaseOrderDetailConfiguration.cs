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
    public class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<PurchaseOrderDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetail> entity)
        {
            entity.ToTable("PurchaseOrderDetail", "Purchasing");
            entity.HasKey(e => new { e.PurchaseOrderId, e.PurchaseOrderDetailId })
                    .HasName("PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID");

            entity.HasComment("Individual products associated with a specific purchase order. See PurchaseOrderHeader.");

            entity.HasIndex(e => e.ProductId);

            entity.Property(e => e.PurchaseOrderId).HasComment("Primary key. Foreign key to PurchaseOrderHeader.PurchaseOrderID.");

            entity.Property(e => e.PurchaseOrderDetailId)
                .HasComment("Primary key. One line number per purchased product.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DueDate).HasComment("Date the product is expected to be received.");

            entity.Property(e => e.LineTotal)
                .HasComment("Per product subtotal. Computed as OrderQty * UnitPrice.")
                .HasComputedColumnSql("(isnull([OrderQty]*[UnitPrice],(0.00)))");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderQty).HasComment("Quantity ordered.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.ReceivedQty).HasComment("Quantity actually received from the vendor.");

            entity.Property(e => e.RejectedQty).HasComment("Quantity rejected during inspection.");

            entity.Property(e => e.StockedQty)
                .HasComment("Quantity accepted into inventory. Computed as ReceivedQty - RejectedQty.")
                .HasComputedColumnSql("(isnull([ReceivedQty]-[RejectedQty],(0.00)))");

            entity.Property(e => e.UnitPrice).HasComment("Vendor's selling price of a single product.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.PurchaseOrderDetail)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderDetail)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
