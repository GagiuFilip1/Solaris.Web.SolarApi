using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Extensions;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Data;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Repositories.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class PlanetRepository : IPlanetRepository
    {
        private readonly DataContext m_dataContext;

        public PlanetRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(Planet entity)
        {
            await m_dataContext.Planets.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Planet entity)
        {
            m_dataContext.Planets.Update(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Planet entity)
        {
            m_dataContext.Planets.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<Planet>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<Planet> filtering)
        {
            return await filtering.Filter(m_dataContext.Planets.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}