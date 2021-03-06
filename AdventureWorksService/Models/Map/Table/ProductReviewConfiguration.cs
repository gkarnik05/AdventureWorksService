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
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> entity)
        {
            entity.ToTable("ProductReview", "Production");
            entity.HasComment("Customer reviews of products they have purchased.");

            entity.HasIndex(e => new { e.Comments, e.ProductId, e.ReviewerName })
                .HasName("IX_ProductReview_ProductID_Name");

            entity.Property(e => e.ProductReviewId).HasComment("Primary key for ProductReview records.");

            entity.Property(e => e.Comments).HasComment("Reviewer's comments");

            entity.Property(e => e.EmailAddress).HasComment("Reviewer's e-mail address.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.ProductId).HasComment("Product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.Rating).HasComment("Product rating given by the reviewer. Scale is 1 to 5 with 5 as the highest rating.");

            entity.Property(e => e.ReviewDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date review was submitted.");

            entity.Property(e => e.ReviewerName).HasComment("Name of the reviewer.");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductReview)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
