using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AdventureWorksService.WebApi
{
    public class Program
    {        
        public static void Main(string[] args)
        {                      
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                var settings = config.Build();
                var appConfigEndpoint = settings.GetConnectionString("AppConfig");
                
                config.AddAzureAppConfiguration(options =>
                {
                    options.Connect(appConfigEndpoint);
                    options.ConfigureRefresh(refresh =>
                        refresh.Register("Serilog:PrimaryKey")
                            .SetCacheExpiration(TimeSpan.FromSeconds(10)));
                    
                }).Build();             
                                               
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5001, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;           
                    });
                });
                
                webBuilder.UseStartup<Startup>();

            });
    }
}
