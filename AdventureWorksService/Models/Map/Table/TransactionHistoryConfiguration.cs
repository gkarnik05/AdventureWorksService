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
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> entity)
        {
            entity.ToTable("TransactionHistory", "Production");
            entity.HasKey(e => e.TransactionId)
                    .HasName("PK_TransactionHistory_TransactionID");

            entity.HasComment("Record of each purchase order, sales order, or work order transaction year to date.");

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => new { e.ReferenceOrderId, e.ReferenceOrderLineId });

            entity.Property(e => e.TransactionId).HasComment("Primary key for TransactionHistory records.");

            entity.Property(e => e.ActualCost).HasComment("Product cost.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.Quantity).HasComment("Product quantity.");

            entity.Property(e => e.ReferenceOrderId).HasComment("Purchase order, sales order, or work order identification number.");

            entity.Property(e => e.ReferenceOrderLineId).HasComment("Line number associated with the purchase order, sales order, or work order.");

            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time of the transaction.");

            entity.Property(e => e.TransactionType)
                .IsFixedLength()
                .HasComment("W = WorkOrder, S = SalesOrder, P = PurchaseOrder");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.TransactionHistory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
