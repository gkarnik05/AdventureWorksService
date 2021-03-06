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
    public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> entity)
        {
            entity.ToTable("ProductModel", "Production");
            entity.HasComment("Product model classification.");

            entity.HasIndex(e => e.CatalogDescription)
                .HasName("PXML_ProductModel_CatalogDescription");

            entity.HasIndex(e => e.Instructions)
                .HasName("PXML_ProductModel_Instructions");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ProductModel_Name")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductModel_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductModelId).HasComment("Primary key for ProductModel records.");

            entity.Property(e => e.CatalogDescription).HasComment("Detailed product catalog information in xml format.");

            entity.Property(e => e.Instructions).HasComment("Manufacturing instructions in xml format.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Product model description.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }
    }
}
