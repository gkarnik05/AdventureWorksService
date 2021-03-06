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
    public class SalesReasonConfiguration : IEntityTypeConfiguration<SalesReason>
    {
        public void Configure(EntityTypeBuilder<SalesReason> entity)
        {
            entity.ToTable("SalesReason", "Sales");
            entity.HasComment("Lookup table of customer purchase reasons.");

            entity.Property(e => e.SalesReasonId).HasComment("Primary key for SalesReason records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Sales reason description.");

            entity.Property(e => e.ReasonType).HasComment("Category the sales reason belongs to.");
        }
    }
}
