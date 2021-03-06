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
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> entity)
        {
            entity.ToTable("ContactType", "Person");
            entity.HasComment("Lookup table containing the types of business entity contacts.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ContactType_Name")
                .IsUnique();

            entity.Property(e => e.ContactTypeId).HasComment("Primary key for ContactType records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Contact type description.");
        }
    }
}
