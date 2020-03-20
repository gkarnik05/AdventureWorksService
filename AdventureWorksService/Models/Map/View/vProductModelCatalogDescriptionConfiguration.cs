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
    public class vProductModelCatalogDescriptionConfiguration : IEntityTypeConfiguration<VProductModelCatalogDescription>
    {
        public void Configure(EntityTypeBuilder<VProductModelCatalogDescription> entity)
        {
            entity.HasNoKey();

            entity.ToView("vProductModelCatalogDescription", "Production");

            entity.HasComment("Displays the content from each element in the xml column CatalogDescription for each product in the Production.ProductModel table that has catalog data.");

            entity.Property(e => e.ProductModelId).ValueGeneratedOnAdd();
        }
    }
}
