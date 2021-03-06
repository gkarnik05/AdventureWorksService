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
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> entity)
        {
            entity.ToTable("ShoppingCartItem", "Sales");
            entity.HasComment("Contains online customer orders until the order is submitted or cancelled.");

            entity.HasIndex(e => new { e.ShoppingCartId, e.ProductId });

            entity.Property(e => e.ShoppingCartItemId).HasComment("Primary key for ShoppingCartItem records.");

            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date the time the record was created.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ProductId).HasComment("Product ordered. Foreign key to Product.ProductID.");

            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("((1))")
                .HasComment("Product quantity ordered.");

            entity.Property(e => e.ShoppingCartId).HasComment("Shopping cart identification number.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ShoppingCartItem)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
