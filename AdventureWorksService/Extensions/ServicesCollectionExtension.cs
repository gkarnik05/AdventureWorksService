using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AdventureWorksService.WebApi.Interfaces;
using AdventureWorksService.WebApi.Services;
using Serilog;

namespace AdventureWorksService.WebApi.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection RegisterAdventureWorksServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProductionService, ProductionService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ISalesService, SalesService>();

            return services;
        }       
        
    }
}
