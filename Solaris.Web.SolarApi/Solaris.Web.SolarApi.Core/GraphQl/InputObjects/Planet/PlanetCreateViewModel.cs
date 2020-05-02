using GraphQL.Types;

namespace Solaris.Web.SolarApi.Core.GraphQl.InputObjects.Planet
{
    public class PlanetCreateViewModel : InputObjectGraphType<Models.Entities.Planet>
    {
        public PlanetCreateViewModel()
        {
            Field(x => x.Name);
            Field(x => x.SolarSystemId, type: typeof(NonNullGraphType<IdGraphType>)).Description("The id of the solar system this planet is located");
            Field(x => x.Description, true);
            Field(x => x.ImageUrl, true, typeof(UriGraphType)).Description("The url of the image of planet");
            Field(x => x.PlanetStatus, true).Description("The current exploration status over this planet (Unexplored = 0,ExplorationInProcess = 1,Habitable = 2,Uninhabitable = 3)");
            Field(x => x.PlanetRadius, true).Description("Represents the radius of the planet in km");
            Field(x => x.GravityForce, true).Description("Represents the gravity force of the planet in m/s^2");
            Field(x => x.OxygenPercentage, true).Description("The oxygen percentage of the planet ");
            Field(x => x.SpinFrequency, true).Description("The time amount the planets takes to spin once, measured in minutes");
            Field(x => x.TemperatureDay, true).Description("The planet temperature on the hemisphere facing the sun, measured in Celsius");
            Field(x => x.TemperatureNight, true).Description("The planet temperature on the hemisphere not facing the sun, measured in Celsius");
            Field(x => x.WaterPercentage, true).Description("The planet water percentage that can be found on the crust and near it");
            Field(x => x.PlanetSurfaceMagneticField, true).Description("The Planet Magnetic field strength measured in Ampere/m (A/m)");
            Field(x => x.AverageSolarWindVelocity, true).Description("The average speed of solar winds hitting the atmosphere of the planet");
        }
    }
}