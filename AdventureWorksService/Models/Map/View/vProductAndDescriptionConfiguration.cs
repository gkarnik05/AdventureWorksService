using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdventureWorksService.WebApi.Contract;

namespace AdventureWorksService.WebApi.Models.Map
{
    public class vProductAndDescriptionConfiguration : IEntityTypeConfiguration<VProductAndDescription>
    {
        public void Configure(EntityTypeBuilder<VProductAndDescription> entity)
        {
            entity.HasNoKey();

            entity.ToView("vProductAndDescription", "Production");

            entity.HasComment("Product names and descriptions. Product descriptions are provided in multiple languages.");

            entity.Property(e => e.CultureId).IsFixedLength();
        }
    }
}
