using System.Collections.Generic;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.OutputObjects.Planet;
using Solaris.Web.SolarApi.Core.GraphQl.OutputObjects.SolarSystem;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class ListResponseType<T> : ObjectGraphType<object> where T : IGraphType
    {
        protected ListResponseType()
        {
            Field<IntGraphType>().Name("totalCount")
                .Description("A count of the total number of objects in this connection, ignoring pagination.");
            Field<ListGraphType<T>>().Name("items")
                .Description("A list of all of the objects returned in the connection.");
        }
    }

    public class ListResponse<T>
    {
        public long TotalCount { get; set; }
        public IList<T> Items { get; set; }
    }

    public class ListPlanetsQueryModelType : ListResponseType<PlanetQueryViewModel>
    {
    }

    public class ListSolarSystemsQueryModelType : ListResponseType<SolarSystemQueryViewModel>
    {
    }
}