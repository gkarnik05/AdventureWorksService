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
            services.Configure<AzureAdConfig>(Configuration.GetSection("AzureAd"));
            //Logging           
            ConfigureLogging(SerilogConfig);

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });

            //HttpContext
            services.AddHttpContextAccessor();

            //Services
            services.RegisterAdventureWorksServices();

            //Database
            services.AddDbContext<AdventureWorks2017DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Sql")));
            services.AddScoped(sp => new Func<AdventureWorks2017DbContext>(() =>
            sp.GetRequiredService<AdventureWorks2017DbContext>()));

            //Automapper
            services.AddAutoMapper(typeof(Startup));

            //Authentication
            AddAuthentication(services,Configuration, AzureAdConfig);
            //Authorization
            AddAuthorization(services, Configuration);
            //Swagger
            AddSwagger(services, AzureAdConfig);
                       
        }
        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration,AzureAdConfig azureAdConfig)
        {
            services.AddAuthentication(options=> {
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

        private static void AddAuthorization(IServiceCollection services, IConfiguration configuration)
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

        private static void ConfigureLogging(SerilogConfig serilogConfig)
        {
            Log.Logger = new LoggerConfiguration()
                           .Enrich.FromLogContext()
                           .Enrich.With(new EnvironmentEnricher(serilogConfig.Environment))
                           .WriteTo.Console()
                           .WriteTo.Seq(serilogConfig.ServiceUrl)
                           .CreateLogger();
        }
        private static void AddSwagger(IServiceCollection services, AzureAdConfig azureAdConfig)
        {
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdventureWorks API", Version = "v1" });
                c.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{azureAdConfig.Instance}{azureAdConfig.TenantId}/oauth2/v2.0/authorize", UriKind.Absolute),
                            TokenUrl = new Uri($"{azureAdConfig.Instance}{azureAdConfig.TenantId}/oauth2/v2.0/token", UriKind.Absolute),
                            Scopes = DelegatedPermissions.All.ToDictionary(p => $"{azureAdConfig.Audience}/{p}")
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
                                    { Type = ReferenceType.SecurityScheme, Id = SecuritySchemeType.OAuth2.ToString().ToLower() } },
                                    new[] { $"{azureAdConfig.Audience}"
                            }
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

            app.UseHttpsRedirection();
            
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();                
            });
            UseSwagger(app, AzureAdConfig);
            
        }

        private static void UseSwagger(IApplicationBuilder app,AzureAdConfig azureAdConfig)
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
