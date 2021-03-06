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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee", "HumanResources");
            entity.HasKey(e => e.BusinessEntityId)
                    .HasName("PK_Employee_BusinessEntityID");

            entity.HasComment("Employee information such as salary, department, and title.");

            entity.HasIndex(e => e.LoginId)
                .HasName("AK_Employee_LoginID")
                .IsUnique();

            entity.HasIndex(e => e.NationalIdnumber)
                .HasName("AK_Employee_NationalIDNumber")
                .IsUnique();

            entity.HasIndex(e => e.Rowguid)
                .HasName("AK_Employee_rowguid")
                .IsUnique();

            entity.Property(e => e.BusinessEntityId)
                .HasComment("Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.")
                .ValueGeneratedNever();

            entity.Property(e => e.BirthDate).HasComment("Date of birth.");

            entity.Property(e => e.CurrentFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("0 = Inactive, 1 = Active");

            entity.Property(e => e.Gender)
                .IsFixedLength()
                .HasComment("M = Male, F = Female");

            entity.Property(e => e.HireDate).HasComment("Employee hired on this date.");

            entity.Property(e => e.JobTitle).HasComment("Work title such as Buyer or Sales Representative.");

            entity.Property(e => e.LoginId).HasComment("Network login.");

            entity.Property(e => e.MaritalStatus)
                .IsFixedLength()
                .HasComment("M = Married, S = Single");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.NationalIdnumber).HasComment("Unique national identification number such as a social security number.");

            entity.Property(e => e.OrganizationLevel)
                .HasComment("The depth of the employee in the corporate hierarchy.")
                .HasComputedColumnSql("([OrganizationNode].[GetLevel]())");

            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            entity.Property(e => e.SalariedFlag)
                .HasDefaultValueSql("((1))")
                .HasComment("Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");

            entity.Property(e => e.SickLeaveHours).HasComment("Number of available sick leave hours.");

            entity.Property(e => e.VacationHours).HasComment("Number of available vacation hours.");

            entity.HasOne(d => d.BusinessEntity)
                .WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
