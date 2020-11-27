using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Services.AppAuthentication;
using AdventureWorksService.WebApi.Config;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using AdventureWorksService.WebApi.Authorization;
using AdventureWorksService.WebApi.Interfaces;

namespace AdventureWorksService.WebApi.Models
{    
    public class AdventureWorks2017DbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
                               
        public AdventureWorks2017DbContext(DbContextOptions options, ILoggerFactory loggerFactory): base (options)
        {
            this._loggerFactory = loggerFactory;            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }       
        
    }
}
