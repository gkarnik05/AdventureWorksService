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
    public class ProductModelIllustrationConfiguration : IEntityTypeConfiguration<ProductModelIllustration>
    {
        public void Configure(EntityTypeBuilder<ProductModelIllustration> entity)
        {
            entity.ToTable("ProductModelIllustration", "Production");
            entity.HasKey(e => new { e.ProductModelId, e.IllustrationId })
                    .HasName("PK_ProductModelIllustration_ProductModelID_IllustrationID");

            entity.HasComment("Cross-reference table mapping product models and illustrations.");

            entity.Property(e => e.ProductModelId).HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

            entity.Property(e => e.IllustrationId).HasComment("Primary key. Foreign key to Illustration.IllustrationID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.Illustration)
                .WithMany(p => p.ProductModelIllustration)
                .HasForeignKey(d => d.IllustrationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.ProductModelIllustration)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
