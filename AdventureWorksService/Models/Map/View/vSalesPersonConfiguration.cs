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
    public class vSalesPersonConfiguration : IEntityTypeConfiguration<VSalesPerson>
    {
        public void Configure(EntityTypeBuilder<VSalesPerson> entity)
        {
            entity.HasNoKey();

            entity.ToView("vSalesPerson", "Sales");

            entity.HasComment("Sales representiatives (names and addresses) and their sales-related information.");
        }
    }
}
