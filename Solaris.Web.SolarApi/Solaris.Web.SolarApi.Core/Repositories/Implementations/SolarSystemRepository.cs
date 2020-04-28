using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Data;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Extensions;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Models;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Core.Repositories.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class SolarSystemRepository : ISolarSystemRepository
    {
        private readonly DataContext m_dataContext;

        public SolarSystemRepository(DataContext dataContext)
        {
            m_dataContext = dataContext;
        }

        public async Task CreateAsync(SolarSystem entity)
        {
            await m_dataContext.SolarSystems.AddAsync(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(SolarSystem entity)
        {
            m_dataContext.SolarSystems.Update(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SolarSystem entity)
        {
            m_dataContext.SolarSystems.Remove(entity);
            await m_dataContext.SaveChangesAsync();
        }

        public async Task<Tuple<int, List<SolarSystem>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<SolarSystem> filtering)
        {
            return await filtering.Filter(m_dataContext.SolarSystems.AsQueryable())
                .WithOrdering(ordering, new Ordering())
                .WithPaginationAsync(pagination);
        }
    }
}