using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Models;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Core.Repositories.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class PlanetRepository : IPlanetRepository
    {
        public Task CreateAsync(Planet entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Planet entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Planet entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tuple<int, List<Planet>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Planet> filtering)
        {
            throw new NotImplementedException();
        }
    }
}