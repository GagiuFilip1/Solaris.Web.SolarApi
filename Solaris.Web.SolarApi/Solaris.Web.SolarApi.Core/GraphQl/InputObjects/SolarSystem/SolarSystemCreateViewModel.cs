using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;

namespace Solaris.Web.SolarApi.Core.GraphQl.InputObjects.SolarSystem
{
    public class SolarSystemCreateViewModel : InputObjectGraphType<Models.Entities.SolarSystem>
    {
        public SolarSystemCreateViewModel()
        {
            Field(x => x.Name).Description("The name of Solar System must contains only Upper case letters and symbols");
            Field(x => x.SpacePosition, false, typeof(NonNullGraphType<SpaceCoordinatesInputGraphType>)).Description("The x, y, z position of the system");
            Field(x => x.DistanceToEarth, false, typeof(FloatGraphType)).Description("The distance in light years to earth");
        }
    }
}