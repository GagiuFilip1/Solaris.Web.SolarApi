using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers;

namespace Solaris.Web.SolarApi.Infrastructure.Data
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
            SetConvertors(modelBuilder);
        }

        private static void SetModelsRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasOne(t => t.SolarSystem)
                .WithMany(t => t.Planets);
        }

        private static void SetConvertors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolarSystem>().Property(t => t.SpacePosition).HasConversion(new ValueConverter<SpaceCoordinates, string>(
                t => t == null ? null : t.ToString(),
                t => t == null ? new SpaceCoordinates() : SpaceCoordinates.FromString(t)
            ));
        }
    }
}