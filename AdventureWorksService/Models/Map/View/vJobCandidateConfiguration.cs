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
    public class vJobCandidateConfiguration : IEntityTypeConfiguration<VJobCandidate>
    {
        public void Configure(EntityTypeBuilder<VJobCandidate> entity)
        {
            entity.HasNoKey();

            entity.ToView("vJobCandidate", "HumanResources");

            entity.HasComment("Job candidate names and resumes.");

            entity.Property(e => e.JobCandidateId).ValueGeneratedOnAdd();
        }
    }
}
