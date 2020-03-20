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
    public class vEmployeeDepartmentConfiguration : IEntityTypeConfiguration<VEmployeeDepartment>
    {
        public void Configure(EntityTypeBuilder<VEmployeeDepartment> entity)
        {            
            entity.HasNoKey();

            entity.ToView("vEmployeeDepartment", "HumanResources");

            entity.HasComment("Returns employee name, title, and current department.");
        }
    }
}
