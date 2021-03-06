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
    public class ProductDescriptionConfiguration : IEntityTypeConfiguration<ProductDescription>
    {
        public void Configure(EntityTypeBuilder<ProductDescription> entity)
        {
            entity.ToTable("ProductDescription", "Production");
            entity.HasComment("Product descriptions in several languages.");

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_ProductDescription_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductDescriptionId).HasComment("Primary key for ProductDescription records.");

            entity.Property(e => e.Description).HasComment("Description of the product.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
        }
    }
}
