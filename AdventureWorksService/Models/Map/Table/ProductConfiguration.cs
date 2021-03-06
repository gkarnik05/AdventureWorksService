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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasComment("Products sold or used in the manfacturing of sold products.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Product_Name")
                .IsUnique();

            entity.HasIndex(e => e.ProductNumber)
                .HasName("AK_Product_ProductNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Product_rowguid")
                .IsUnique();

            entity.Property(e => e.ProductId).HasComment("Primary key for Product records.");

            entity.Property(e => e.Class)
                .IsFixedLength()
                .HasComment("H = High, M = Medium, L = Low");

            entity.Property(e => e.Color).HasComment("Product color.");

            entity.Property(e => e.DaysToManufacture).HasComment("Number of days required to manufacture the product.");

            entity.Property(e => e.DiscontinuedDate).HasComment("Date the product was discontinued.");

            entity.Property(e => e.FinishedGoodsFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Product is not a salable item. 1 = Product is salable.");

            entity.Property(e => e.ListPrice).HasComment("Selling price.");

            entity.Property(e => e.MakeFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Product is purchased, 1 = Product is manufactured in-house.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Name of the product.");

            entity.Property(e => e.ProductLine)
                .IsFixedLength()
                .HasComment("R = Road, M = Mountain, T = Touring, S = Standard");

            entity.Property(e => e.ProductModelId).HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");

            entity.Property(e => e.ProductNumber).HasComment("Unique product identification number.");

            entity.Property(e => e.ProductSubcategoryId).HasComment("Product is a member of this product subcategory. Foreign key to ProductSubCategory.ProductSubCategoryID. ");

            entity.Property(e => e.ReorderPoint).HasComment("Inventory level that triggers a purchase order or work order. ");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SafetyStockLevel).HasComment("Minimum inventory quantity. ");

            entity.Property(e => e.SellEndDate).HasComment("Date the product was no longer available for sale.");

            entity.Property(e => e.SellStartDate).HasComment("Date the product was available for sale.");

            entity.Property(e => e.Size).HasComment("Product size.");

            entity.Property(e => e.SizeUnitMeasureCode)
                .IsFixedLength()
                .HasComment("Unit of measure for Size column.");

            entity.Property(e => e.StandardCost).HasComment("Standard cost of the product.");

            entity.Property(e => e.Style)
                .IsFixedLength()
                .HasComment("W = Womens, M = Mens, U = Universal");

            entity.Property(e => e.Weight).HasComment("Product weight.");

            entity.Property(e => e.WeightUnitMeasureCode)
                .IsFixedLength()
                .HasComment("Unit of measure for Weight column.");
        }
    }
}
