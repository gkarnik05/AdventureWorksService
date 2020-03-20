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
    public class vEmployeeConfiguration : IEntityTypeConfiguration<VEmployee>
    {
        public void Configure(EntityTypeBuilder<VEmployee> entity)
        {            
            entity.HasNoKey();

            entity.ToView("vEmployee", "HumanResources");

            entity.HasComment("Employee names and addresses.");
        }
    }
}
