using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;

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
            modelBuilder.Entity<SolarSystem>().Property(t => t.SpacePosition).HasConversion(new ValueConverter<Vector3, string>(
                t => t == null ? null : JsonConvert.SerializeObject(t),
                t => t == null ? new Vector3() : JsonConvert.DeserializeObject<Vector3>(t)
            ));
        }
    }
}