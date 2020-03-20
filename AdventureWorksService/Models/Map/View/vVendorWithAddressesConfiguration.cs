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
    public class vVendorWithAddressesConfiguration : IEntityTypeConfiguration<VVendorWithAddresses>
    {
        public void Configure(EntityTypeBuilder<VVendorWithAddresses> entity)
        {
            entity.HasNoKey();

            entity.ToView("vVendorWithAddresses", "Purchasing");

            entity.HasComment("Vendor (company) names and addresses .");
        }
    }
}
