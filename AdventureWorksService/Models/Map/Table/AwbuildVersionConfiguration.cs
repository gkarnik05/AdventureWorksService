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
    public class AwbuildVersionConfiguration : IEntityTypeConfiguration<AwbuildVersion>
    {
        public void Configure(EntityTypeBuilder<AwbuildVersion> entity)
        {
            entity.ToTable("AwbuildVersion");
            entity.HasKey(e => e.SystemInformationId)
                    .HasName("PK_AWBuildVersion_SystemInformationID");

            entity.HasComment("Current version number of the AdventureWorks 2016 sample database. ");

            entity.Property(e => e.SystemInformationId)
                .HasComment("Primary key for AWBuildVersion records.")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.DatabaseVersion).HasComment("Version number of the database in 9.yy.mm.dd.00 format.");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.");

            entity.Property(e => e.VersionDate).HasComment("Date and time the record was last updated.");
        }
    }
}
