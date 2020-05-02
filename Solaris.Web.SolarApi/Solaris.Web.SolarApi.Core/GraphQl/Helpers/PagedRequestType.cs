using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class PagedRequestType : InputObjectGraphType<Pagination>
    {
        public new static readonly string Description = "Pagination Settings";

        public PagedRequestType()
        {
            Field(x => x.Take, true).Description("Count of items to return");
            Field(x => x.Offset, true).Description("Count of items to skip");
        }
    }
}