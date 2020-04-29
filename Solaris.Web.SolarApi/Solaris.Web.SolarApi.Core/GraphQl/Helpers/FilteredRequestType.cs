using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class FilteredRequestType<T> : InputObjectGraphType<IFilter<T>> where T : IIdentifier
    {
        public new static readonly string Description = $"Filter to be applied when searching for {typeof(T).FullName}";

        public FilteredRequestType()
        {
            Field(x => x.SearchTerm, true).Description("The value used to filter the results");
        }
    }
}