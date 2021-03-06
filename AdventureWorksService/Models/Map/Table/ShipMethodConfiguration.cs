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
    public class ShipMethodConfiguration : IEntityTypeConfiguration<ShipMethod>
    {
        public void Configure(EntityTypeBuilder<ShipMethod> entity)
        {
            entity.ToTable("ShipMethod", "Purchasing");
            entity.HasComment("Shipping company lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ShipMethod_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ShipMethod_rowguid")
                .IsUnique();

            entity.Property(e => e.ShipMethodId).HasComment("Primary key for ShipMethod records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Shipping company name.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.ShipBase)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Minimum shipping charge.");

            entity.Property(e => e.ShipRate)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Shipping charge per pound.");
        }
    }
}
