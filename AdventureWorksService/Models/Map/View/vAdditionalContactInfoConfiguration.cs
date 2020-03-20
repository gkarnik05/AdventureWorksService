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
    public class vAdditionalContactInfoConfiguration : IEntityTypeConfiguration<VAdditionalContactInfo>
    {
        public void Configure(EntityTypeBuilder<VAdditionalContactInfo> entity)
        {            
            entity.HasNoKey();

            entity.ToView("vAdditionalContactInfo", "Person");

            entity.HasComment("Displays the contact name and content from each element in the xml column AdditionalContactInfo for that person.");
        }
    }
}
