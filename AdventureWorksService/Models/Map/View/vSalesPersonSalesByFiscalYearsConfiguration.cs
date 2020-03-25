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
    public class vSalesPersonSalesByFiscalYearsConfiguration : IEntityTypeConfiguration<VSalesPersonSalesByFiscalYears>
    {
        public void Configure(EntityTypeBuilder<VSalesPersonSalesByFiscalYears> entity)
        {
            entity.HasNoKey();

            entity.ToView("vSalesPersonSalesByFiscalYears", "Sales");                       

            entity.HasComment("Uses PIVOT to return aggregated sales information for each sales representative.");
        }
    }
}
