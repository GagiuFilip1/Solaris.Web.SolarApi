using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Infrastructure.Data;

namespace Solaris.Web.SolarApi.Tests.Utils
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            DataContext = new DataContext(options);
            SeedDataBase().Wait();
        }

        public DataContext DataContext { get; }

        public void Dispose()
        {
            DataContext.Database.EnsureDeleted();
            DataContext.Dispose();
        }

        private async Task SeedDataBase()
        {
            var solarSystems = DatabaseSeed.GetSolarSystems();
            var planets = DatabaseSeed.GetPlanets();
            await DataContext.SolarSystems.AddRangeAsync(solarSystems);
            await DataContext.Planets.AddRangeAsync(planets);
            await DataContext.SaveChangesAsync();
        }
    }
}