using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;

namespace Solaris.Web.SolarApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelsRelations(modelBuilder);
            SetConvertors(modelBuilder);
            SetIndexes(modelBuilder);
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

        private static void SetIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolarSystem>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Planet>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}