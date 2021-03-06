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
    public class SalesTerritoryHistoryConfiguration : IEntityTypeConfiguration<SalesTerritoryHistory>
    {
        public void Configure(EntityTypeBuilder<SalesTerritoryHistory> entity)
        {
            entity.ToTable("SalesTerritoryHistory", "Sales");
            entity.HasKey(e => new { e.BusinessEntityId, e.StartDate, e.TerritoryId })
                    .HasName("PK_SalesTerritoryHistory_BusinessEntityID_StartDate_TerritoryID");

            entity.HasComment("Sales representative transfers to other sales territories.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesTerritoryHistory_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key. The sales rep.  Foreign key to SalesPerson.BusinessEntityID.");

            entity.Property(e => e.StartDate).HasComment("Primary key. Date the sales representive started work in the territory.");

            entity.Property(e => e.TerritoryId).HasComment("Primary key. Territory identification number. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.Property(e => e.EndDate).HasComment("Date the sales representative left work in the territory.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.SalesTerritoryHistory)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Territory)
                .WithMany(p => p.SalesTerritoryHistory)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
