using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdventureWorksService.WebApi.Data;

namespace AdventureWorksService.WebApi.Models.Map
{
    public class BillOfMaterialsConfiguration : IEntityTypeConfiguration<BillOfMaterials>
    {
        public void Configure(EntityTypeBuilder<BillOfMaterials> entity)
        {
            entity.HasKey(e => e.BillOfMaterialsId)
                    .HasName("PK_BillOfMaterials_BillOfMaterialsID")
                    .IsClustered(false);

            entity.HasComment("Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components.");

            entity.HasIndex(e => e.UnitMeasureCode);

            entity.HasIndex(e => new { e.ProductAssemblyId, e.ComponentId, e.StartDate })
                .HasName("AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.BillOfMaterialsId).HasComment("Primary key for BillOfMaterials records.");

            entity.Property(e => e.Bomlevel).HasComment("Indicates the depth the component is from its parent (AssemblyID).");

            entity.Property(e => e.ComponentId).HasComment("Component identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.EndDate).HasComment("Date the component stopped being used in the assembly item.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PerAssemblyQty)
                .HasDefaultValueSql("((1.00))")
                .HasComment("Quantity of the component needed to create the assembly.");

            entity.Property(e => e.ProductAssemblyId).HasComment("Parent product identification number. Foreign key to Product.ProductID.");

            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date the component started being used in the assembly item.");

            entity.Property(e => e.UnitMeasureCode)
                .IsFixedLength()
                .HasComment("Standard code identifying the unit of measure for the quantity.");

            entity.HasOne(d => d.Component)
                .WithMany(p => p.BillOfMaterialsComponent)
                .HasForeignKey(d => d.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.UnitMeasureCodeNavigation)
                .WithMany(p => p.BillOfMaterials)
                .HasForeignKey(d => d.UnitMeasureCode)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
