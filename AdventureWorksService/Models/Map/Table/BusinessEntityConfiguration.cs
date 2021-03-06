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
    public class BusinessEntityConfiguration : IEntityTypeConfiguration<BusinessEntity>
    {
        public void Configure(EntityTypeBuilder<BusinessEntity> entity)
        {
            entity.ToTable("BusinessEntity", "Person");
            entity.HasComment("Source of the ID that connects vendors, customers, and employees with address and contact information.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_BusinessEntity_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key for all customers, vendors, and employees.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }
    }
}
