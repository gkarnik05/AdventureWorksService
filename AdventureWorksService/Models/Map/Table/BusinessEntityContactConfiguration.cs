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
    public class BusinessEntityContactConfiguration : IEntityTypeConfiguration<BusinessEntityContact>
    {
        public void Configure(EntityTypeBuilder<BusinessEntityContact> entity)
        {
            entity.ToTable("BusinessEntityContact", "Person");
            entity.HasKey(e => new { e.BusinessEntityId, e.PersonId, e.ContactTypeId })
                    .HasName("PK_BusinessEntityContact_BusinessEntityID_PersonID_ContactTypeID");

            entity.HasComment("Cross-reference table mapping stores, vendors, and employees to people");

            entity.HasIndex(e => e.ContactTypeId);

            entity.HasIndex(e => e.PersonId);

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_BusinessEntityContact_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key. Foreign key to BusinessEntity.BusinessEntityID.");

            entity.Property(e => e.PersonId).HasComment("Primary key. Foreign key to Person.BusinessEntityID.");

            entity.Property(e => e.ContactTypeId).HasComment("Primary key.  Foreign key to ContactType.ContactTypeID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.BusinessEntityContact)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ContactType)
                .WithMany(p => p.BusinessEntityContact)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.BusinessEntityContact)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
