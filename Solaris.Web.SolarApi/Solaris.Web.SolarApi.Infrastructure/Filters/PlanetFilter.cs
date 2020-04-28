using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Core.Extensions;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Infrastructure.Filters
{
    public class PlanetFilter : IFilter<Planet>
    {
        public string SearchTerm { get; set; }

        public IQueryable<Planet> Filter(IQueryable<Planet> filterQuery)
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return filterQuery;

            filterQuery = Guid.TryParse(SearchTerm, out var guid)
                ? filterQuery.Where(p => p.Id.Equals(guid) || p.SolarSystemId.Equals(guid))
                : filterQuery.Where(p =>
                    EF.Functions.Like(p.Name, SearchTerm.ToMySqlLikeSyntax()));
          
            return filterQuery;
        }
    }
}