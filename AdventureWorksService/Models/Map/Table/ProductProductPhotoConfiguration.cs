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
    public class ProductProductPhotoConfiguration : IEntityTypeConfiguration<ProductProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductProductPhoto> entity)
        {
            entity.ToTable("ProductProductPhoto", "Production");
            entity.HasKey(e => new { e.ProductId, e.ProductPhotoId })
                    .HasName("PK_ProductProductPhoto_ProductID_ProductPhotoID")
                    .IsClustered(false);

            entity.HasComment("Cross-reference table mapping products and product photos.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.ProductPhotoId).HasComment("Product photo identification number. Foreign key to ProductPhoto.ProductPhotoID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Primary).HasComment("0 = Photo is not the principal image. 1 = Photo is the principal image.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductProductPhoto)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductPhoto)
                .WithMany(p => p.ProductProductPhoto)
                .HasForeignKey(d => d.ProductPhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
