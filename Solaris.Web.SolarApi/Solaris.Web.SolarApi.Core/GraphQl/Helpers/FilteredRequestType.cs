using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Helpers;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class FilteredRequestType<T> : InputObjectGraphType<IFilter<T>> where T : IIdentifier
    {
        public new const string Description = "";

        public FilteredRequestType()
        {
            Field(x => x.SearchTerm, true);
        }
    }
}