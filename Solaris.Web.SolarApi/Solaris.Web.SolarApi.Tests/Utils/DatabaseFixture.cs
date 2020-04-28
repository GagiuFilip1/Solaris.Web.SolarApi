using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Core.Data;

namespace Solaris.Web.SolarApi.Tests.Utils
{
    public class DatabaseFixture : IDisposable
    {
        public DataContext DataContext { get; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            DataContext = new DataContext(options);
            SeedDataBase().Wait();
        }

        private async Task SeedDataBase()
        {
            var solarSystems = DataBaseSeed.GetSolarSystems();
            var planets = DataBaseSeed.GetPlanets();
            await DataContext.SolarSystems.AddRangeAsync(solarSystems);
            await DataContext.Planets.AddRangeAsync(planets);
            await DataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DataContext.Database.EnsureDeleted();
            DataContext.Dispose();
        }
    }
}