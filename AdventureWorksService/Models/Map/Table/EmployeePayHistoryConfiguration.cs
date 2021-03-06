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
    public class EmployeePayHistoryConfiguration : IEntityTypeConfiguration<EmployeePayHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeePayHistory> entity)
        {
            entity.ToTable("EmployeePayHistory", "HumanResources");
            entity.HasKey(e => new { e.BusinessEntityId, e.RateChangeDate })
                     .HasName("PK_EmployeePayHistory_BusinessEntityID_RateChangeDate");

            entity.HasComment("Employee pay history.");

            entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.RateChangeDate).HasComment("Date the change in pay is effective");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.PayFrequency).HasComment("1 = Salary received monthly, 2 = Salary received biweekly");

            entity.Property(e => e.Rate).HasComment("Salary hourly rate.");

            entity.HasOne(d => d.BusinessEntity)
                .WithMany(p => p.EmployeePayHistory)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
