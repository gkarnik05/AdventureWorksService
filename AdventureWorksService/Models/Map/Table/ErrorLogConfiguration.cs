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
    public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> entity)
        {
            entity.ToTable("ErrorLog", "dbo");
            entity.HasComment("Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct. Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.");

            entity.Property(e => e.ErrorLogId).HasComment("Primary key for ErrorLog records.");

            entity.Property(e => e.ErrorLine).HasComment("The line number at which the error occurred.");

            entity.Property(e => e.ErrorMessage).HasComment("The message text of the error that occurred.");

            entity.Property(e => e.ErrorNumber).HasComment("The error number of the error that occurred.");

            entity.Property(e => e.ErrorProcedure).HasComment("The name of the stored procedure or trigger where the error occurred.");

            entity.Property(e => e.ErrorSeverity).HasComment("The severity of the error that occurred.");

            entity.Property(e => e.ErrorState).HasComment("The state number of the error that occurred.");

            entity.Property(e => e.ErrorTime)
                .HasDefaultValueSql("(getdate())")
                .HasComment("The date and time at which the error occurred.");

            entity.Property(e => e.UserName).HasComment("The user who executed the batch in which the error occurred.");
        }
    }
}
