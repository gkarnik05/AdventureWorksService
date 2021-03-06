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
    public class IllustrationConfiguration : IEntityTypeConfiguration<Illustration>
    {
        public void Configure(EntityTypeBuilder<Illustration> entity)
        {
            entity.ToTable("Illustration", "Production");
            entity.HasComment("Bicycle assembly diagrams.");

            entity.Property(e => e.IllustrationId).HasComment("Primary key for Illustration records.");

            entity.Property(e => e.Diagram).HasComment("Illustrations used in manufacturing instructions. Stored as XML.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");
        }
    }
}
