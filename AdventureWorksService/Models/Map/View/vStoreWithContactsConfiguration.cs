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
    public class vStoreWithContactsConfiguration : IEntityTypeConfiguration<VStoreWithContacts>
    {
        public void Configure(EntityTypeBuilder<VStoreWithContacts> entity)
        {
            entity.HasNoKey();

            entity.ToView("vStoreWithContacts", "Sales");

            entity.HasComment("Stores (including store contacts) that sell Adventure Works Cycles products to consumers.");
        }
    }
}
