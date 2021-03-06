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
    public class CultureConfiguration : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> entity)
        {
            entity.ToTable("Culture", "Production");
            entity.HasComment("Lookup table containing the languages in which some AdventureWorks data is stored.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Culture_Name")
                .IsUnique();

            entity.Property(e => e.CultureId)
                .IsFixedLength()
                .HasComment("Primary key for Culture records.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Culture description.");
        }
    }
}
