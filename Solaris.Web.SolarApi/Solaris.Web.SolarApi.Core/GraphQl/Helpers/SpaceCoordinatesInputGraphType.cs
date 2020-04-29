using System.Numerics;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Helpers;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public class SpaceCoordinatesInputGraphType : InputObjectGraphType<SpaceCoordinates>
    {
        public SpaceCoordinatesInputGraphType()
        {
            Field(t => t.XCoordonate, false, typeof(FloatGraphType)).Description("The X Space Position");
            Field(t => t.YCoordonate, false,  typeof(FloatGraphType)).Description("The Y Space Position");
            Field(t => t.ZCoordonate, false,  typeof(FloatGraphType)).Description("The Z Space Position");
        }
    }

    public class SpaceCoordinatesOutputGraphType : ObjectGraphType<SpaceCoordinates>
    {
        public SpaceCoordinatesOutputGraphType()
        {
            Field(t => t.XCoordonate, true).Description("The X Space Position");
            Field(t => t.YCoordonate, true).Description("The Y Space Position");
            Field(t => t.ZCoordonate, true).Description("The Z Space Position");
        }
    }
}