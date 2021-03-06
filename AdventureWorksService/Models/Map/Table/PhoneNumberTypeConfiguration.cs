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
    public class PhoneNumberTypeConfiguration : IEntityTypeConfiguration<PhoneNumberType>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberType> entity)
        {
            entity.ToTable("PhoneNumberType", "Person");
            entity.HasComment("Type of phone number of a person.");

            entity.Property(e => e.PhoneNumberTypeId).HasComment("Primary key for telephone number type records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Name of the telephone number type");
        }
    }
}
