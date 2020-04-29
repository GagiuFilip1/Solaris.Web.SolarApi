using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;

namespace Solaris.Web.SolarApi.Core.GraphQl.OutputObjects.SolarSystem
{
    public class SolarSystemQueryViewModel : ObjectGraphType<Models.Entities.SolarSystem>
    {
        public SolarSystemQueryViewModel()
        {
            Field(x => x.Id, type: typeof(NonNullGraphType<GuidGraphType>));
            Field(x => x.Name);
            Field(x => x.SpacePosition, false, typeof(SpaceCoordinatesOutputGraphType)).Description("The x, y, z position of the system");
            Field(x => x.DistanceToEarth, false, typeof(FloatGraphType)).Description("The distance in light years to earth");
        }
    }
}