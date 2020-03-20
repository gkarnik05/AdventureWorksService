using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdventureWorksService.WebApi.Extensions;
using AdventureWorksService.WebApi.Models;
using Serilog;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace AdventureWorksService.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                           .Enrich.FromLogContext()
                           .Enrich.With(new EnvironmentEnricher(Configuration["Logging:Serilog:Environment"]))
                           .WriteTo.Console()
                           .WriteTo.Seq(Configuration["Logging:Serilog:ServiceUrl"])
                           .CreateLogger();

            services.AddControllers();
            services.RegisterAdventureWorksServices();
            services.AddDbContext<AdventureWorks2017DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Sql")));
            services.AddScoped(sp => new Func<AdventureWorks2017DbContext>(() =>
            sp.GetRequiredService<AdventureWorks2017DbContext>()));
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();
            services.AddMvc()
             .AddNewtonsoftJson(x =>
             {
                 x.SerializerSettings.ContractResolver = new DefaultContractResolver
                 {
                     NamingStrategy = new SnakeCaseNamingStrategy()
                 };
             });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks API", Version = "v1" });                
            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdventureWorks API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
