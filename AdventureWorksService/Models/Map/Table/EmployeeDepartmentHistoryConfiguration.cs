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
    public class EmployeeDepartmentHistoryConfiguration : IEntityTypeConfiguration<EmployeeDepartmentHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeDepartmentHistory> entity)
        {
            entity.ToTable("EmployeeDepartmentHistory", "HumanResources");
            entity.HasKey(e => new { e.BusinessEntityId, e.StartDate, e.DepartmentId, e.ShiftId })
                    .HasName("PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID");

            entity.HasComment("Employee department transfers.");

            entity.HasIndex(e => e.DepartmentId);

            entity.HasIndex(e => e.ShiftId);

            entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.StartDate).HasComment("Date the employee started work in the department.");

            entity.Property(e => e.DepartmentId).HasComment("Department in which the employee worked including currently. Foreign key to Department.DepartmentID.");

            entity.Property(e => e.ShiftId).HasComment("Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID.");

            entity.Property(e => e.EndDate).HasComment("Date the employee left the department. NULL = Current department.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmployeeDepartmentHistory)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Department)
                .WithMany(p => p.EmployeeDepartmentHistory)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Shift)
                .WithMany(p => p.EmployeeDepartmentHistory)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
