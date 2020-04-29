using System.Collections.Generic;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;

namespace Solaris.Web.SolarApi.Core.GraphQl.Root
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery(IEnumerable<ISchemaGroup> queries)
        {
            Name = "Query";
            foreach (var query in queries)
            {
                query.SetGroup(this);
            }
        }
    }
}