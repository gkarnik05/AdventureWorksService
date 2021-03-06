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
    public class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> entity)
        {
            entity.ToTable("AddressType", "Person");
            entity.HasComment("Types of addresses stored in the Address table. ");

            entity.HasIndex(e => e.Name)
                .HasName("AK_AddressType_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_AddressType_rowguid")
                .IsUnique();

            entity.Property(e => e.AddressTypeId).HasComment("Primary key for AddressType records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Address type description. For example, Billing, Home, or Shipping.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }
    }
}
