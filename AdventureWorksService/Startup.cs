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
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AdventureWorksService.WebApi.Config;
using System.Security.Cryptography;
using System.Text;
using AdventureWorksService.WebApi.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using AdventureWorksService.WebApi.Common;
using AdventureWorksService.WebApi.Interfaces;
using AdventureWorksService.WebApi.Services;

namespace AdventureWorksService.WebApi
{
    public class Startup
    {

        public IConfiguration Configuration { get;}
        public AzureAdConfig AzureAdConfig { get;}
        public SerilogConfig SerilogConfig { get;}        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
            SerilogConfig = configuration.GetSection("Serilog").Get<SerilogConfig>();
            AzureAdConfig = configuration.GetSection("AzureAd").Get<AzureAdConfig>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 1. Register the in-memory cache
            services.AddMemoryCache();

            // 2. Register the Azure Identity-based token provider
            services.AddSingleton<IAzureSqlTokenProvider,AzureIdentityAzureSqlTokenProvider>();

            // 3. Register a caching decorator using the Scrutor NuGet package
            services.Decorate<IAzureSqlTokenProvider, CacheAzureSqlTokenProvider>();

            // 4. Finally, register the interceptor
            services.AddSingleton<AadAuthenticationDbConnectionInterceptor>();

            services.Configure<AzureAdConfig>(Configuration.GetSection("AzureAd"));
            services.AddApplicationInsightsTelemetry(Configuration.GetSection("ApplicationInsights"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            //Logging           
            ConfigureLogging(SerilogConfig, AzureAdConfig);

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });

            services.AddGrpc();

            //HttpContext
            services.AddHttpContextAccessor();

            //Services
            services.RegisterAdventureWorksServices();

            //Database
            services.AddDbContext<AdventureWorks2017DbContext>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Sql"), options=>
                {
                    options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(30), null);                        
                });
                options.AddInterceptors(provider.GetRequiredService<AadAuthenticationDbConnectionInterceptor>());
            });

            services.AddScoped(sp => new Func<AdventureWorks2017DbContext>(() =>
                    sp.GetRequiredService<AdventureWorks2017DbContext>()));

            //Automapper
            services.AddAutoMapper(typeof(Startup));

            //Authentication
            AddAuthentication(services,Configuration, AzureAdConfig);
            //Authorization
            AddAuthorization(services);
            //Swagger
            AddSwagger(services, AzureAdConfig);
                       
        }
        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration,AzureAdConfig azureAdConfig)
        {
            services.AddAuthentication(options=> 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddAzureADBearer(options =>
            {
                configuration.Bind("AzureAd", options);
            })
            .AddJwtBearer(options =>
            {
                options.Audience = azureAdConfig.Audience;
                options.Authority = $"{azureAdConfig.Instance}{azureAdConfig.TenantId}/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = $"{azureAdConfig.Instance}{azureAdConfig.TenantId}/",
                    ValidateIssuerSigningKey = true,
                    SaveSigninToken = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true
                };
            });
            services.AddSingleton<IClaimsTransformation, ScopeClaimSplitTransformation>();
        }

        private static void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options=> {
                // Require callers to have at least one valid permission by default
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .Build();

                // Create a policy for each action that can be done in the API
                foreach (string action in Actions.All)
                {
                    options.AddPolicy(action, policy => policy.AddRequirements(new ActionAuthorizationRequirement(action)));
                }
            });
            services.AddSingleton<IAuthorizationHandler, AnyValidPermissionRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, ActionAuthorizationRequirementHandler>();
        }

        private static void ConfigureLogging(SerilogConfig serilogConfig, AzureAdConfig azureAdConfig)
        {            
            Log.Logger = new LoggerConfiguration()                           
                           .Enrich.FromLogContext()                           
                           .Enrich.With(new EnvironmentEnricher(serilogConfig.Environment))
                           .WriteTo.Console()                           
                           .WriteTo.AzureAnalytics(serilogConfig.WorkspaceId,serilogConfig.PrimaryKey,
                           new Serilog.Sinks.AzureAnalytics.ConfigurationSettings { LogName= azureAdConfig.ApplicationName })
                           .CreateLogger();
        }
        private static void AddSwagger(IServiceCollection services, AzureAdConfig azureAdConfig)
        {
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{azureAdConfig.Instance}{azureAdConfig.TenantId}/oauth2/v2.0/authorize", UriKind.Absolute),
                            TokenUrl = new Uri($"{azureAdConfig.Instance}{azureAdConfig.TenantId}/oauth2/v2.0/token", UriKind.Absolute),
                            Scopes = DelegatedPermissions.All
                                    .Select(p => new KeyValuePair<string,string>($"{azureAdConfig.Audience}/{p}",p))
                                    .ToDictionary(p=>p.Key,p => p.Value)                                    
                        }                        
                    },
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    { 
                                      Type = ReferenceType.SecurityScheme, 
                                      Id = SecuritySchemeType.OAuth2.ToString().ToLower() 
                                } 
                            },
                            new[] { $"{azureAdConfig.Audience}" }
                    }
                });

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
            
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
                     
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapGrpcService<EmployeeService>();
                endpoints.MapGrpcService<ProductionService>();
                endpoints.MapGrpcService<SalesService>();
            });

            app.UseGrpcWeb();

            UseSwagger(app, AzureAdConfig);
            
        }

        private static void UseSwagger(IApplicationBuilder app, AzureAdConfig azureAdConfig)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdventureWorks API V1");
                c.OAuthConfigObject.ClientId = azureAdConfig.ClientId;
                c.OAuthConfigObject.ClientSecret = azureAdConfig.ClientSecret;
                c.OAuthScopeSeparator(" ");
                c.ConfigObject.AdditionalItems.Add("grant_type", "client_credentials");
                c.OAuthConfigObject.AdditionalQueryStringParams = new Dictionary<string, string>();
                c.OAuthRealm(azureAdConfig.ClientId);
                c.OAuthConfigObject.AdditionalQueryStringParams.Add("prompt", "consent");
                c.OAuthConfigObject.AdditionalQueryStringParams.Add("client_secret", azureAdConfig.ClientSecret);
                c.OAuthAppName(azureAdConfig.ApplicationName);
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
