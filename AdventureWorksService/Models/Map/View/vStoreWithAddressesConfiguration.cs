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
    public class vStoreWithAddressesConfiguration : IEntityTypeConfiguration<VStoreWithAddresses>
    {
        public void Configure(EntityTypeBuilder<VStoreWithAddresses> entity)
        {
            entity.HasNoKey();

            entity.ToView("vStoreWithAddresses", "Sales");

            entity.HasComment("Stores (including store addresses) that sell Adventure Works Cycles products to consumers.");
        }
    }
}
