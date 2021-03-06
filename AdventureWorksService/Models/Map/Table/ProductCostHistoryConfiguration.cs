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
    public class ProductCostHistoryConfiguration : IEntityTypeConfiguration<ProductCostHistory>
    {
        public void Configure(EntityTypeBuilder<ProductCostHistory> entity)
        {
            entity.ToTable("ProductCostHistory", "Production");
            entity.HasKey(e => new { e.ProductId, e.StartDate })
                    .HasName("PK_ProductCostHistory_ProductID_StartDate");

            entity.HasComment("Changes in the cost of a product over time.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID");

            entity.Property(e => e.StartDate).HasComment("Product cost start date.");

            entity.Property(e => e.EndDate).HasComment("Product cost end date.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.StandardCost).HasComment("Standard cost of the product.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductCostHistory)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
