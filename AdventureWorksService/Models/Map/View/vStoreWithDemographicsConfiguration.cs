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
    public class vStoresWithDemographicsConfiguration : IEntityTypeConfiguration<VStoreWithDemographics>
    {
        public void Configure(EntityTypeBuilder<VStoreWithDemographics> entity)
        {
            entity.HasNoKey();

            entity.ToView("vStoreWithDemographics", "Sales");

            entity.HasComment("Stores (including demographics) that sell Adventure Works Cycles products to consumers.");
        }
    }
}
