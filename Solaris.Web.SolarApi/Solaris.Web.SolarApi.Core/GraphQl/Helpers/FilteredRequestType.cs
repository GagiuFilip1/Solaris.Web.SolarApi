using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Filters.Interfaces;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class FilteredRequestType<T> : InputObjectGraphType<IFilter<T>> where T : IIdentifier
    {
        public new const string Description = "Filter used to refine the search response";

        public FilteredRequestType()
        {
            Field(x => x.SearchTerm, true).Description($"The search term which will be used to filter the list of {typeof(T).Name}s");
        }
    }
}