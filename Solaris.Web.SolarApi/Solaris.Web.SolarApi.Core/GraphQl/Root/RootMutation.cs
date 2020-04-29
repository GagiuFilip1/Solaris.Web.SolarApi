using System.Collections.Generic;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;

namespace Solaris.Web.SolarApi.Core.GraphQl.Root
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation(IEnumerable<ISchemaGroup> mutations)
        {
            Name = "Mutation";
            foreach (var mutation in mutations)
            {
                mutation.SetGroup(this);
            }
        }
    }
}