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
    public class BusinessEntityAddressConfiguration : IEntityTypeConfiguration<BusinessEntityAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityAddress> entity)
        {
            entity.ToTable("BusinessEntityAddress","Person");
            entity.HasKey(e => new { e.BusinessEntityId, e.AddressId, e.AddressTypeId })
                    .HasName("PK_BusinessEntityAddress_BusinessEntityID_AddressID_AddressTypeID");

            entity.HasComment("Cross-reference table mapping customers, vendors, and employees to their addresses.");

            entity.HasIndex(e => e.AddressId);

            entity.HasIndex(e => e.AddressTypeId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_BusinessEntityAddress_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key. Foreign key to BusinessEntity.BusinessEntityID.");

            entity.Property(e => e.AddressId).HasComment("Primary key. Foreign key to Address.AddressID.");

            entity.Property(e => e.AddressTypeId).HasComment("Primary key. Foreign key to AddressType.AddressTypeID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.BusinessEntityAddress)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.AddressType)
                .WithMany(p => p.BusinessEntityAddress)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.BusinessEntityAddress)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
