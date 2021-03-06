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
    public class PersonCreditCardConfiguration : IEntityTypeConfiguration<PersonCreditCard>
    {
        public void Configure(EntityTypeBuilder<PersonCreditCard> entity)
        {
            entity.ToTable("PersonCreditCard", "Sales");
            entity.HasKey(e => new { e.BusinessEntityId, e.CreditCardId })
                    .HasName("PK_PersonCreditCard_BusinessEntityID_CreditCardID");

            entity.HasComment("Cross-reference table mapping people to their credit card information in the CreditCard table. ");

            entity.Property(e => e.BusinessEntityId).HasComment("Business entity identification number. Foreign key to Person.BusinessEntityID.");

            entity.Property(e => e.CreditCardId).HasComment("Credit card identification number. Foreign key to CreditCard.CreditCardID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.PersonCreditCard)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreditCard)
                .WithMany(p => p.PersonCreditCard)
                .HasForeignKey(d => d.CreditCardId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
