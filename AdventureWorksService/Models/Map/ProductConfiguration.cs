using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorksService.WebApi.Models.Map
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Production");
            builder.HasKey(e => e.ProductID);
            builder.Property(e => e.ProductNumber);
            builder.Property(e => e.MakeFlag);
            builder.Property(e => e.FinishedGoodsFlag);
            builder.Property(e => e.Color);
            builder.Property(e => e.SafetyStockLevel).HasColumnType("smallint");
            builder.Property(e => e.ReorderPoint).HasColumnType("smallint");
            builder.Property(e => e.StandardCost).HasColumnType("money");
            builder.Property(e => e.ListPrice).HasColumnType("money");
            builder.Property(e => e.Size);
        }
    }
}
