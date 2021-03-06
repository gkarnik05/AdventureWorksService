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
    public class SalesPersonConfiguration : IEntityTypeConfiguration<SalesPerson>
    {
        public void Configure(EntityTypeBuilder<SalesPerson> entity)
        {
            entity.ToTable("SalesPerson", "Sales");
            entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_SalesPerson_BusinessEntityID");

            entity.HasComment("Sales representative current information.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SalesPerson_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Primary key for SalesPerson records. Foreign key to Employee.BusinessEntityID")
                .ValueGeneratedNever();

            entity.Property(e => e.Bonus)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Bonus due if quota is met.");

            entity.Property(e => e.CommissionPct)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Commision percent received per sale.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesLastYear)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales total of previous year.");

            entity.Property(e => e.SalesQuota).HasComment("Projected yearly sales.");

            entity.Property(e => e.SalesYtd)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Sales total year to date.");

            entity.Property(e => e.TerritoryId).HasComment("Territory currently assigned to. Foreign key to SalesTerritory.SalesTerritoryID.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.SalesPerson)
                .HasForeignKey<SalesPerson>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
