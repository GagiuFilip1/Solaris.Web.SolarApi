using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Solaris.Web.SolarApi.Core.Data;
using Solaris.Web.SolarApi.Core.Repositories.Implementations;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private const string CONNECTION_STRING_PATH = "ConnectionStrings:SolarisApi";
        private const string MIGRATION_ASSEMBLY = "Solaris.Web.SolarApi.Presentation";
        private const string REPOSITORIES_NAMESPACE = "Solaris.Web.SolarApi.Core.Repositories.Implementations";
        private const string SERVICES_NAMESPACE = "Solaris.Web.SolarApi.Core.Services.Implementations";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InjectMySqlDbContext<DataContext>(Configuration[CONNECTION_STRING_PATH], MIGRATION_ASSEMBLY);
            services.InjectForNamespace(REPOSITORIES_NAMESPACE);
            services.InjectForNamespace(SERVICES_NAMESPACE);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}