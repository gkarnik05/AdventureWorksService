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
    public class JobCandidateConfiguration : IEntityTypeConfiguration<JobCandidate>
    {
        public void Configure(EntityTypeBuilder<JobCandidate> entity)
        {
            entity.ToTable("JobCandidate", "HumanResources");
            entity.HasComment("Résumés submitted to Human Resources by job applicants.");

            entity.HasIndex(e => e.BusinessEntityId);

            entity.Property(e => e.JobCandidateId).HasComment("Primary key for JobCandidate records.");

            entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.Resume).HasComment("Résumé in XML format.");
        }
    }
}
