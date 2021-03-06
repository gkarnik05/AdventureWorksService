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
    public class PurchaseOrderHeaderConfiguration : IEntityTypeConfiguration<PurchaseOrderHeader>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderHeader> entity)
        {
            entity.ToTable("PurchaseOrderHeader", "Purchasing");
            entity.HasKey(e => e.PurchaseOrderId)
                    .HasName("PK_PurchaseOrderHeader_PurchaseOrderID");

            entity.HasComment("General purchase order information. See PurchaseOrderDetail.");

            entity.HasIndex(e => e.EmployeeId);

            entity.HasIndex(e => e.VendorId);

            entity.Property(e => e.PurchaseOrderId).HasComment("Primary key.");

            entity.Property(e => e.EmployeeId).HasComment("Employee who created the purchase order. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.Freight)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Shipping cost.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Purchase order creation date.");

            entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the purchase order over time.");

            entity.Property(e => e.ShipDate).HasComment("Estimated shipment date from the vendor.");

            entity.Property(e => e.ShipMethodId).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

            entity.Property(e => e.Status)
                .HasDefaultValueSql("((1))")
                .HasComment("Order current status. 1 = Pending; 2 = Approved; 3 = Rejected; 4 = Complete");

            entity.Property(e => e.SubTotal)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Purchase order subtotal. Computed as SUM(PurchaseOrderDetail.LineTotal)for the appropriate PurchaseOrderID.");

            entity.Property(e => e.TaxAmt)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Tax amount.");

            entity.Property(e => e.TotalDue)
                .HasComment("Total due to vendor. Computed as Subtotal + TaxAmt + Freight.")
                .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

            entity.Property(e => e.VendorId).HasComment("Vendor with whom the purchase order is placed. Foreign key to Vendor.BusinessEntityID.");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.PurchaseOrderHeader)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipMethod)
                .WithMany(p => p.PurchaseOrderHeader)
                .HasForeignKey(d => d.ShipMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Vendor)
                .WithMany(p => p.PurchaseOrderHeader)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
