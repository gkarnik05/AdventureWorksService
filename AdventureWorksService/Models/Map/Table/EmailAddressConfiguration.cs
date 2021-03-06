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
    public class EmailAddressConfiguration : IEntityTypeConfiguration<EmailAddress>
    {
        public void Configure(EntityTypeBuilder<EmailAddress> entity)
        {
            entity.ToTable("EmailAddress", "Person");
            entity.HasKey(e => new { e.BusinessEntityId, e.EmailAddressId })
                     .HasName("PK_EmailAddress_BusinessEntityID_EmailAddressID");

            entity.HasComment("Where to send a person email.");

            entity.HasIndex(e => e.EmailAddress1);

            entity.Property(e => e.BusinessEntityId).HasComment("Primary key. Person associated with this email address.  Foreign key to Person.BusinessEntityID");

            entity.Property(e => e.EmailAddressId)
                .HasComment("Primary key. ID of this email address.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EmailAddress1).HasComment("E-mail address for the person.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmailAddress)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
