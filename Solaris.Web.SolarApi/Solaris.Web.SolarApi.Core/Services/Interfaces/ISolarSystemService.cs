using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.Services.Interfaces
{
    public interface ISolarSystemService
    {
        Task CreateSolarSystemAsync(SolarSystem solarSystem);
        Task UpdateSolarSystemAsync(SolarSystem solarSystem);
        Task DeleteSolarSystemAsync(SolarSystem solarSystem);
        Task SearchSolarSystemAsync(Pagination pagination, Ordering ordering, IFilter<SolarSystem> filter);
    }
}