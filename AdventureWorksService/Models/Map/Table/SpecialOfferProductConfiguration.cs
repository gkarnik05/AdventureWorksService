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
    public class SpecialOfferProductConfiguration : IEntityTypeConfiguration<SpecialOfferProduct>
    {
        public void Configure(EntityTypeBuilder<SpecialOfferProduct> entity)
        {
            entity.ToTable("SpecialOfferProduct", "Sales");
            entity.HasKey(e => new { e.SpecialOfferId, e.ProductId })
                    .HasName("PK_SpecialOfferProduct_SpecialOfferID_ProductID");

            entity.HasComment("Cross-reference table mapping products to special offer discounts.");

            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_SpecialOfferProduct_rowguid")
                .IsUnique();

            entity.Property(e => e.SpecialOfferId).HasComment("Primary key for SpecialOfferProduct records.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.SpecialOfferProduct)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SpecialOffer)
                .WithMany(p => p.SpecialOfferProduct)
                .HasForeignKey(d => d.SpecialOfferId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
