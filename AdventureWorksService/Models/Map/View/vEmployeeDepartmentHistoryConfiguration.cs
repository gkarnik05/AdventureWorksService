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
    public class vEmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<VEmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<VEmployeeDepartmentHistory> entity)
        {
            entity.HasNoKey();

            entity.ToView("vEmployeeDepartmentHistory", "HumanResources");

            entity.HasComment("Returns employee name and current and previous departments.");
        }
    }
}
