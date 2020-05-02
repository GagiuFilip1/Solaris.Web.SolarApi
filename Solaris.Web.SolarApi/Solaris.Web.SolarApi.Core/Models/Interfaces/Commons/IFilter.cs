using System.Linq;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces.Commons
{
    public interface IFilter<TEntity> where TEntity : IIdentifier
    {
        public string SearchTerm { get; set; }
        IQueryable<TEntity> Filter(IQueryable<TEntity> filterQuery);
    }
}