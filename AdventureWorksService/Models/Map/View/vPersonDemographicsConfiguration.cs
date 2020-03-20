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
    public class VPersonDemographicsConfiguration : IEntityTypeConfiguration<VPersonDemographics>
    {
        public void Configure(EntityTypeBuilder<VPersonDemographics> entity)
        {
            entity.HasNoKey();

            entity.ToView("vPersonDemographics", "Sales");

            entity.HasComment("Displays the content from each element in the xml column Demographics for each customer in the Person.Person table.");
        }
    }
}
