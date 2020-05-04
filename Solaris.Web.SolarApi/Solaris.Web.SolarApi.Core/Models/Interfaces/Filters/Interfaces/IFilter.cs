using System.Linq;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces.Filters.Interfaces
{
    public interface IFilter<TEntity> where TEntity : IIdentifier
    {
        public string SearchTerm { get; set; }
        IQueryable<TEntity> Filter(IQueryable<TEntity> filterQuery);
    }
}