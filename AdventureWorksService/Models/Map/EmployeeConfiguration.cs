using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdventureWorksService.WebApi.Models.Map
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {           
            builder.ToTable("Employee","HumanResources");
            builder.HasKey(e => e.BusinessEntityId);
            builder.Property(e => e.BirthDate);
            builder.Property(e => e.Gender);
            builder.Property(e => e.JobTitle);
            builder.Property(e => e.LoginId);
            builder.Property(e => e.MaritalStatus);
            builder.Property(e => e.NationalIdNumber);
        }
    }
}
