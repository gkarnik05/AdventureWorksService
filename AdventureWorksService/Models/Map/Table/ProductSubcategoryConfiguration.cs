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
    public class ProductSubcategoryConfiguration : IEntityTypeConfiguration<ProductSubcategory>
    {
        public void Configure(EntityTypeBuilder<ProductSubcategory> entity)
        {
            entity.ToTable("ProductSubcategory", "Production");
            entity.HasComment("Product subcategories. See ProductCategory table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ProductSubcategory_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductSubcategory_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductSubcategoryId).HasComment("Primary key for ProductSubcategory records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Subcategory description.");

            entity.Property(e => e.ProductCategoryId).HasComment("Product category identification number. Foreign key to ProductCategory.ProductCategoryID.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.ProductCategory)
                .WithMany(p => p.ProductSubcategory)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
