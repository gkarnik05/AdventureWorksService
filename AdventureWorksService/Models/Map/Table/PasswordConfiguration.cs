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
    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> entity)
        {
            entity.ToTable("Password", "Person");
            entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Password_BusinessEntityID");

            entity.HasComment("One way hashed authentication information");

            entity.Property(e => e.BusinessEntityId).ValueGeneratedNever();

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PasswordHash)
                .IsUnicode(false)
                .HasComment("Password for the e-mail account.");

            entity.Property(e => e.PasswordSalt)
                .IsUnicode(false)
                .HasComment("Random value concatenated with the password string before the password is hashed.");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Password)
                .HasForeignKey<Password>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
