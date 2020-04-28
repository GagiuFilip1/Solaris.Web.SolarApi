using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Core.Models;

namespace Solaris.Web.SolarApi.Core.Data
{
    public class DataContext : DbContext
    {
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Planet> Planets { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelsRelations(modelBuilder);
        }

        private static void SetModelsRelations(ModelBuilder modelBuilder)
        {
           
        }
    }
}