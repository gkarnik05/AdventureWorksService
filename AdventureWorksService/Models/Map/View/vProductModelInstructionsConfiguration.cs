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
    public class vProductModelInstructionsConfiguration : IEntityTypeConfiguration<VProductModelInstructions>
    {
        public void Configure(EntityTypeBuilder<VProductModelInstructions> entity)
        {
            entity.HasNoKey();

            entity.ToView("vProductModelInstructions", "Production");

            entity.HasComment("Displays the content from each element in the xml column Instructions for each product in the Production.ProductModel table that has manufacturing instructions.");

            entity.Property(e => e.ProductModelId).ValueGeneratedOnAdd();
        }
    }
}
