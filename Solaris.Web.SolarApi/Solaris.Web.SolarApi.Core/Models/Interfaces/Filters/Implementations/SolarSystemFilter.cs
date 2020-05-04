using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Core.Extensions;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Filters.Interfaces;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces.Filters.Implementations
{
    public class SolarSystemFilter : IFilter<SolarSystem>
    {
        public string SearchTerm { get; set; }

        public IQueryable<SolarSystem> Filter(IQueryable<SolarSystem> filterQuery)
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return filterQuery;

            filterQuery = Guid.TryParse(SearchTerm, out var guid)
                ? filterQuery.Where(p => p.Id.Equals(guid))
                : filterQuery.Where(p =>
                    EF.Functions.Like(p.Name, SearchTerm.ToMySqlLikeSyntax()) ||
                    EF.Functions.Like(p.SpacePosition.ToString(), SearchTerm.ToMySqlLikeSyntax()));

            return filterQuery;
        }
    }
}