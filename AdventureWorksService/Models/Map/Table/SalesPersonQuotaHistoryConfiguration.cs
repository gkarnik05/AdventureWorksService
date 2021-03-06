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
    public class SalesPersonQuotaHistoryConfiguration : IEntityTypeConfiguration<SalesPersonQuotaHistory>
    {
        public void Configure(EntityTypeBuilder<SalesPersonQuotaHistory> entity)
        {
            entity.ToTable("SalesPersonQuotaHistory", "Sales");
            entity.HasKey(e => new { e.BusinessEntityId, e.QuotaDate })
                    .HasName("PK_SalesPersonQuotaHistory_BusinessEntityID_QuotaDate");

            entity.HasComment("Sales performance tracking.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesPersonQuotaHistory_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId).HasComment("Sales person identification number. Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.QuotaDate).HasComment("Sales quota date.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesQuota).HasComment("Sales quota amount.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.SalesPersonQuotaHistory)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
