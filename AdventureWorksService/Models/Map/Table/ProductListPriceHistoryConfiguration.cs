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
    public class ProductListPriceHistoryConfiguration : IEntityTypeConfiguration<ProductListPriceHistory>
    {
        public void Configure(EntityTypeBuilder<ProductListPriceHistory> entity)
        {
            entity.ToTable("ProductListPriceHistory", "Production");
            entity.HasKey(e => new { e.ProductId, e.StartDate })
                    .HasName("PK_ProductListPriceHistory_ProductID_StartDate");

            entity.HasComment("Changes in the list price of a product over time.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID");

            entity.Property(e => e.StartDate).HasComment("List price start date.");

            entity.Property(e => e.EndDate).HasComment("List price end date");

            entity.Property(e => e.ListPrice).HasComment("Product list price.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductListPriceHistory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
