using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Solaris.Web.SolarApi.Core.Data;

namespace Solaris.Web.SolarApi.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private const string SOLUTION_NAME = "Solaris";
        private const string CONNECTION_STRING_PATH = "ConnectionStrings:SolarisApi";
        private const string MIGRATION_ASSEMBLY = "Solaris.Web.SolarApi.Presentation";
        private List<Assembly> Assemblies { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(t => t.FullName.Contains(SOLUTION_NAME))
                .ToList();

            services.AddDbContext<DataContext>(options =>
                options.UseMySql(Configuration[CONNECTION_STRING_PATH],
                    b => b.MigrationsAssembly(MIGRATION_ASSEMBLY)
                        .ServerVersion(new ServerVersion(new Version(5, 7, 12)))
                        .CharSet(CharSet.Latin1)
                ));
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