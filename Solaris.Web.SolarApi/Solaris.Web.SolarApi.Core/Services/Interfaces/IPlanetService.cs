using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.Services.Interfaces
{
    public interface IPlanetService
    {
        Task CreateSolarSystemAsync(Planet planet);
        Task UpdateSolarSystemAsync(Planet planet);
        Task DeleteSolarSystemAsync(Planet planet);
        Task SearchSolarSystemAsync(Pagination pagination, Ordering ordering, IFilter<Planet> filter);
    }
}