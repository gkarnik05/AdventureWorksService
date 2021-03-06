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
    public class ProductModelProductDescriptionCultureConfiguration : IEntityTypeConfiguration<ProductModelProductDescriptionCulture>
    {
        public void Configure(EntityTypeBuilder<ProductModelProductDescriptionCulture> entity)
        {
            entity.ToTable("ProductModelProductDescriptionCulture", "Production");
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.CultureId })
                    .HasName("PK_ProductModelProductDescriptionCulture_ProductModelID_ProductDescriptionID_CultureID");

            entity.HasComment("Cross-reference table mapping product descriptions and the language the description is written in.");

            entity.Property(e => e.ProductModelId).HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

            entity.Property(e => e.ProductDescriptionId).HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.");

            entity.Property(e => e.CultureId)
                .IsFixedLength()
                .HasComment("Culture identification number. Foreign key to Culture.CultureID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.Culture)
                .WithMany(p => p.ProductModelProductDescriptionCulture)
                .HasForeignKey(d => d.CultureId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductDescription)
                .WithMany(p => p.ProductModelProductDescriptionCulture)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel)
                .WithMany(p => p.ProductModelProductDescriptionCulture)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
