using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
