using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReportViewer.Api.Services;
using ReportViewer.Domain.Abstractions;
using ReportViewer.Infrastructure;

namespace ReportViewer.Api
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddControllers();

            services.AddTransient<IDataAccess, DataAccess>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IOrganizationService, OrganizationService>();

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddDataProtection();

            services.AddCors(x =>
            {
                x.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:19006").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                        {
                            //get JsonWebKeySet from AWS
                            var json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                            //serialize result
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json);
                            // cast the result to be the type expected by IssuerSigningKeyResolver
                            return (IEnumerable<SecurityKey>)keys.Keys;
                        },
                        ValidIssuer = "https://cognito-idp.us-east-2.amazonaws.com/us-east-2_GtSWPot4f",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = "4632c3kc3tehhi8gf73cm3414k",
                        ValidateAudience = true,
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
