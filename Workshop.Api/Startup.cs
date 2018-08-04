using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtSharp.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceSharp.AspNetCore;
using Workshop.Api.Models;
using Workshop.Api.Models.Entities;
using Workshop.Api.Models.Services;

namespace Workshop.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // DI (Dependency Injection)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver
                        = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                }); ;

            // API Settings
            var apiSettings = new ApiSettings();
            this.Configuration.Bind(apiSettings);

            services.AddSingleton(apiSettings);

            // Data Context (Database)
            var sqlServerConnectionString = this.Configuration.GetConnectionString("WorkshopEntities");
            services.AddDbContext<WorkshopContext>(options =>
            {
                options.UseSqlServer(sqlServerConnectionString);
            });

            // Services
            services.AddServices();

            // JWT
            services.AddJwtIssuerAndBearer(options =>
            {
                options.Audience = apiSettings.Jwt.Audience;
                options.Issuer = apiSettings.Jwt.Issuer;
                options.SecurityKey = apiSettings.Jwt.SecurityKey;
                options.ExpireSeconds = 3600;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
