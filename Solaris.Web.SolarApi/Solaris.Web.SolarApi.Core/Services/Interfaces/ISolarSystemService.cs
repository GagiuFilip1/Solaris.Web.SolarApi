using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Filters.Interfaces;

namespace Solaris.Web.SolarApi.Core.Services.Interfaces
{
    public interface ISolarSystemService
    {
        Task CreateSolarSystemAsync(SolarSystem solarSystem);
        Task UpdateSolarSystemAsync(SolarSystem solarSystem);
        Task DeleteSolarSystemAsync(Guid id);
        Task<Tuple<int, List<SolarSystem>>> SearchSolarSystemAsync(Pagination pagination, Ordering ordering, IFilter<SolarSystem> filter);
    }
}