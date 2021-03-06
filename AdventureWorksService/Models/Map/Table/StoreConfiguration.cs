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
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.ToTable("Store", "Sales");
            entity.HasKey(e => e.BusinessEntityId)
                     .HasName("PK_Store_BusinessEntityID");

            entity.HasComment("Customers (resellers) of Adventure Works products.");

            entity.HasIndex(e => e.Demographics)
                .HasName("PXML_Store_Demographics");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Store_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.SalesPersonId);

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Primary key. Foreign key to Customer.BusinessEntityID.")
                .ValueGeneratedNever();

            entity.Property(e => e.Demographics).HasComment("Demographic informationg about the store such as the number of employees, annual sales and store type.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Name of the store.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalesPersonId).HasComment("ID of the sales person assigned to the customer. Foreign key to SalesPerson.BusinessEntityID.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Store)
                .HasForeignKey<Store>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
