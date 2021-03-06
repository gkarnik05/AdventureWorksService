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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Address", "Person");
            entity.HasComment("Street address information for customers, employees, and vendors.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Address_rowguid")
                .IsUnique();

            entity.HasIndex(e => e.StateProvinceId);

            entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvinceId, e.PostalCode })
                .IsUnique();

            entity.Property(e => e.AddressId).HasComment("Primary key for Address records.");

            entity.Property(e => e.AddressLine1).HasComment("First street address line.");

            entity.Property(e => e.AddressLine2).HasComment("Second street address line.");

            entity.Property(e => e.City).HasComment("Name of the city.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PostalCode).HasComment("Postal code for the street address.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StateProvinceId).HasComment("Unique identification number for the state or province. Foreign key to StateProvince table.");

            entity.HasOne(d => d.StateProvince)
                .WithMany(p => p.Address)
                .HasForeignKey(d => d.StateProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
