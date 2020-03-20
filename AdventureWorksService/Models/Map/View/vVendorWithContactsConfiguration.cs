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
    public class vVendorWithContactsConfiguration : IEntityTypeConfiguration<VVendorWithContacts>
    {
        public void Configure(EntityTypeBuilder<VVendorWithContacts> entity)
        {
            entity.HasNoKey();

            entity.ToView("vVendorWithContacts", "Purchasing");

            entity.HasComment("Vendor (company) names  and the names of vendor employees to contact.");
        }
    }
}
