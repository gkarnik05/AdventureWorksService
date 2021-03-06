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
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            entity.ToTable("CreditCard", "Sales");
            entity.HasComment("Customer credit card information.");

            entity.HasIndex(e => e.CardNumber)
                .HasName("AK_CreditCard_CardNumber")
                .IsUnique();

            entity.Property(e => e.CreditCardId).HasComment("Primary key for CreditCard records.");

            entity.Property(e => e.CardNumber).HasComment("Credit card number.");

            entity.Property(e => e.CardType).HasComment("Credit card name.");

            entity.Property(e => e.ExpMonth).HasComment("Credit card expiration month.");

            entity.Property(e => e.ExpYear).HasComment("Credit card expiration year.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");
        }
    }
}
