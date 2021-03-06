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
    public class ScrapReasonConfiguration : IEntityTypeConfiguration<ScrapReason>
    {
        public void Configure(EntityTypeBuilder<ScrapReason> entity)
        {
            entity.ToTable("ScrapReason", "Production");
            entity.HasComment("Manufacturing failure reasons lookup table.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_ScrapReason_Name")
                .IsUnique();

            entity.Property(e => e.ScrapReasonId).HasComment("Primary key for ScrapReason records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Failure description.");
        }
    }
}
