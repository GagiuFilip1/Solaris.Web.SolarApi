using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Filters.Interfaces;

namespace Solaris.Web.SolarApi.Core.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IIdentifier
    {
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<Tuple<int, List<TEntity>>> SearchAsync(Pagination pagination, Ordering ordering, IFilter<TEntity> filtering);
    }
}