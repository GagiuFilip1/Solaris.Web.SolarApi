using GraphQL.Types;
using Solaris.Web.SolarApi.Core.Models.Entities;

namespace Solaris.Web.SolarApi.Core.GraphQl.OutputObjects
{
    public class PlanetQueryViewModel : ObjectGraphType<Planet>
    {
        public PlanetQueryViewModel()
        {
            Field(x => x.Id, type: typeof(NonNullGraphType<GuidGraphType>));
            Field(x => x.Description);
            Field(x => x.Name);
            Field(x => x.PlanetStatus).Description("The current exploration status over this planet (Unexplored = 0,ExplorationInProcess = 1,Habitable = 2,Uninhabitable = 3)");
            Field(x => x.SolarSystemId, type: typeof(NonNullGraphType<GuidGraphType>)).Description("The id of the solar system this planet is located");
            Field(x => x.PlanetRadius).Description("Represents the radius of the planet in km");
            Field(x => x.GravityForce).Description("Represents the gravity force of the planet in m/s^2");
            Field(x => x.OxygenPercentage).Description("The oxygen percentage of the planet ");
            Field(x => x.SpinFrequency).Description("The time amount the planets takes to spin once, measured in minutes");
            Field(x => x.TemperatureDay).Description("The planet temperature on the hemisphere facing the sun, measured in Celsius");
            Field(x => x.TemperatureNight).Description("The planet temperature on the hemisphere not facing the sun, measured in Celsius");
            Field(x => x.WaterPercentage).Description("The planet water percentage that can be found on the crust and near it");
            Field(x => x.PlanetSurfaceMagneticField).Description("The Planet Magnetic field strength measured in Ampere/m (A/m)");
            Field(x => x.AverageSolarWindVelocity).Description("The average speed of solar winds hitting the atmosphere of the planet");
        }
    }
}