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
    public class SpecialOfferConfiguration : IEntityTypeConfiguration<SpecialOffer>
    {
        public void Configure(EntityTypeBuilder<SpecialOffer> entity)
        {
            entity.ToTable("SpecialOffer", "Sales");
            entity.HasComment("Sale discounts lookup table.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SpecialOffer_rowguid")
                .IsUnique();

            entity.Property(e => e.SpecialOfferId).HasComment("Primary key for SpecialOffer records.");

            entity.Property(e => e.Category).HasComment("Group the discount applies to such as Reseller or Customer.");

            entity.Property(e => e.Description).HasComment("Discount description.");

            entity.Property(e => e.DiscountPct)
                .HasDefaultValueSql("((0.00))")
                .HasComment("Discount precentage.");

            entity.Property(e => e.EndDate).HasComment("Discount end date.");

            entity.Property(e => e.MaxQty).HasComment("Maximum discount percent allowed.");

            entity.Property(e => e.MinQty).HasComment("Minimum discount percent allowed.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.StartDate).HasComment("Discount start date.");

            entity.Property(e => e.Type).HasComment("Discount type category.");
        }
    }
}
