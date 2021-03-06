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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.ToTable("Customer", "Sales");
            entity.HasComment("Current customer information. Also see the Person and Store tables.");

            entity.HasIndex(e => e.AccountNumber)
                .HasName("AK_Customer_AccountNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Customer_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.TerritoryId);

            entity.Property(e => e.CustomerId).HasComment("Primary key.");

            entity.Property(e => e.AccountNumber)
                .IsUnicode(false)
                .HasComment("Unique number identifying the customer assigned by the accounting system.")
                .HasComputedColumnSql("(isnull('AW'+[dbo].[ufnLeadingZeros]([CustomerID]),''))");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PersonId).HasComment("Foreign key to Person.BusinessEntityID");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StoreId).HasComment("Foreign key to Store.BusinessEntityID");

            entity.Property(e => e.TerritoryId).HasComment("ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");
        }
    }
}
