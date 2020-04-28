using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Extensions;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Interfaces;

namespace Solaris.Web.SolarApi.Core.Models.Filters
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
                    EF.Functions.Like(p.SpacePosition, SearchTerm.ToMySqlLikeSyntax()));

            return filterQuery;
        }
    }
}