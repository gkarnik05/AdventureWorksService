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
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> entity)
        {
            entity.ToTable("WorkOrder", "Production");
            entity.HasComment("Manufacturing work orders.");

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.ScrapReasonId);

            entity.Property(e => e.WorkOrderId).HasComment("Primary key for WorkOrder records.");

            entity.Property(e => e.DueDate).HasComment("Work order due date.");

            entity.Property(e => e.EndDate).HasComment("Work order end date.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.OrderQty).HasComment("Product quantity to build.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.ScrapReasonId).HasComment("Reason for inspection failure.");

            entity.Property(e => e.ScrappedQty).HasComment("Quantity that failed inspection.");

            entity.Property(e => e.StartDate).HasComment("Work order start date.");

            entity.Property(e => e.StockedQty)
                .HasComment("Quantity built and put in inventory.")
                .HasComputedColumnSql("(isnull([OrderQty]-[ScrappedQty],(0)))");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.WorkOrder)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
