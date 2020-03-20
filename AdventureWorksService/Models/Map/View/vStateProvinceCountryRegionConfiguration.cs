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
    public class vStateProvinceCountryRegionConfiguration : IEntityTypeConfiguration<VStateProvinceCountryRegion>
    {
        public void Configure(EntityTypeBuilder<VStateProvinceCountryRegion> entity)
        {
            entity.HasNoKey();

            entity.ToView("vStateProvinceCountryRegion", "Person");

            entity.HasComment("Joins StateProvince table with CountryRegion table.");

            entity.Property(e => e.StateProvinceCode).IsFixedLength();
        }
    }
}
