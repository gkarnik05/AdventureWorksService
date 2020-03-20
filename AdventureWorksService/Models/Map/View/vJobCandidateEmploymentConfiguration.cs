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
    public class vJobCandidateEmploymentConfiguration : IEntityTypeConfiguration<VJobCandidateEmployment>
    {
        public void Configure(EntityTypeBuilder<VJobCandidateEmployment> entity)
        {
            entity.HasNoKey();

            entity.ToView("vJobCandidateEmployment", "HumanResources");

            entity.HasComment("Displays the content from each employement history related element in the xml column Resume in the HumanResources.JobCandidate table. The content has been localized into French, Simplified Chinese and Thai. Some data may not display correctly unless supplemental language support is installed.");

            entity.Property(e => e.JobCandidateId).ValueGeneratedOnAdd();
        }
    }
}
