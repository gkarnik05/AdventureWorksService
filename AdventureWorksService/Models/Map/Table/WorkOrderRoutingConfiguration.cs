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
    public class WorkOrderRoutingConfiguration : IEntityTypeConfiguration<WorkOrderRouting>
    {
        public void Configure(EntityTypeBuilder<WorkOrderRouting> entity)
        {
            entity.ToTable("WorkOrderRouting", "Production");
            entity.HasKey(e => new { e.WorkOrderId, e.ProductId, e.OperationSequence })
                    .HasName("PK_WorkOrderRouting_WorkOrderID_ProductID_OperationSequence");

            entity.HasComment("Work order details.");

            entity.HasIndex(e => e.ProductId);

            entity.Property(e => e.WorkOrderId).HasComment("Primary key. Foreign key to WorkOrder.WorkOrderID.");

            entity.Property(e => e.ProductId).HasComment("Primary key. Foreign key to Product.ProductID.");

            entity.Property(e => e.OperationSequence).HasComment("Primary key. Indicates the manufacturing process sequence.");

            entity.Property(e => e.ActualCost).HasComment("Actual manufacturing cost.");

            entity.Property(e => e.ActualEndDate).HasComment("Actual end date.");

            entity.Property(e => e.ActualResourceHrs).HasComment("Number of manufacturing hours used.");

            entity.Property(e => e.ActualStartDate).HasComment("Actual start date.");

            entity.Property(e => e.LocationId).HasComment("Manufacturing location where the part is processed. Foreign key to Location.LocationID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PlannedCost).HasComment("Estimated manufacturing cost.");

            entity.Property(e => e.ScheduledEndDate).HasComment("Planned manufacturing end date.");

            entity.Property(e => e.ScheduledStartDate).HasComment("Planned manufacturing start date.");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.WorkOrderRouting)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.WorkOrder)
                .WithMany(p => p.WorkOrderRouting)
                .HasForeignKey(d => d.WorkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
