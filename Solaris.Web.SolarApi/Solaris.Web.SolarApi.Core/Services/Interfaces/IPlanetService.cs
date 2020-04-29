using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.Services.Interfaces
{
    public interface IPlanetService
    {
        Task CreatePlanetAsync(Planet planet);
        Task UpdatePlanetAsync(Planet planet);
        Task DeletePlanetAsync(Guid id);
        Task<Tuple<int, List<Planet>>> SearchPlanetAsync(Pagination pagination, Ordering ordering, IFilter<Planet> filter);
    }
}