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
    public class vIndividualCustomerConfiguration : IEntityTypeConfiguration<VIndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<VIndividualCustomer> entity)
        {
            entity.HasNoKey();

            entity.ToView("vIndividualCustomer", "Sales");

            entity.HasComment("Individual customers (names and addresses) that purchase Adventure Works Cycles products online.");
        }
    }
}
