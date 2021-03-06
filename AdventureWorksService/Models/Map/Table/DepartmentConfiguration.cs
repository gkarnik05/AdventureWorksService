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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.ToTable("Department", "HumanResources");
            entity.HasComment("Lookup table containing the departments within the Adventure Works Cycles company.");

            entity.HasIndex(e => e.Name)
                .HasName("AK_Department_Name")
                .IsUnique();

            entity.Property(e => e.DepartmentId).HasComment("Primary key for Department records.");

            entity.Property(e => e.GroupName).HasComment("Name of the group to which the department belongs.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Name).HasComment("Name of the department.");
        }
    }
}
