using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.GraphQl.OutputObjects;
using Solaris.Web.SolarApi.Infrastructure.Data;
using Solaris.Web.SolarApi.Infrastructure.Ioc;
using Solaris.Web.SolarApi.Presentation.GraphQl.Mutations;
using Solaris.Web.SolarApi.Presentation.GraphQl.Queries;

namespace Solaris.Web.SolarApi.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private const string CONNECTION_STRING_PATH = "ConnectionStrings:SolarisApi";
        private const string MIGRATION_ASSEMBLY = "Solaris.Web.SolarApi.Presentation";
        private const string REPOSITORIES_NAMESPACE = "Solaris.Web.SolarApi.Infrastructure.Repositories.Implementations";
        private const string SERVICES_NAMESPACE = "Solaris.Web.SolarApi.Infrastructure.Services.Implementations";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.InjectMySqlDbContext<DataContext>(Configuration[CONNECTION_STRING_PATH], MIGRATION_ASSEMBLY);
            services.InjectForNamespace(REPOSITORIES_NAMESPACE);
            services.InjectForNamespace(SERVICES_NAMESPACE);
            services.InjectGraphQl();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<ISchema>();
        }
    }
}