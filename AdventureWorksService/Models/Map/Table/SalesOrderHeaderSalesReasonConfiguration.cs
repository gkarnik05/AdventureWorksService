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
    public class SalesOrderHeaderSalesReasonConfiguration : IEntityTypeConfiguration<SalesOrderHeaderSalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesOrderHeaderSalesReason> entity)
        {
            entity.ToTable("SalesOrderHeaderSalesReason", "Sales");
            entity.HasKey(e => new { e.SalesOrderId, e.SalesReasonId })
                    .HasName("PK_SalesOrderHeaderSalesReason_SalesOrderID_SalesReasonID");

            entity.HasComment("Cross-reference table mapping sales orders to sales reason codes.");

            entity.Property(e => e.SalesOrderId).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

            entity.Property(e => e.SalesReasonId).HasComment("Primary key. Foreign key to SalesReason.SalesReasonID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.SalesReason)
                .WithMany(p => p.SalesOrderHeaderSalesReason)
                .HasForeignKey(d => d.SalesReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
