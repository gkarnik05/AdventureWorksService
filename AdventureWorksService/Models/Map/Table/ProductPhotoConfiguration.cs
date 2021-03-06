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
    public class ProductPhotoConfiguration : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> entity)
        {
            entity.ToTable("ProductPhoto", "Production");
            entity.HasComment("Product images.");

            entity.Property(e => e.ProductPhotoId).HasComment("Primary key for ProductPhoto records.");

            entity.Property(e => e.LargePhoto).HasComment("Large image of the product.");

            entity.Property(e => e.LargePhotoFileName).HasComment("Large image file name.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");

            entity.Property(e => e.ThumbnailPhotoFileName).HasComment("Small image file name.");
        }
    }
}
