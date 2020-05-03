using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit.Responses;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit.Processors
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class ExplorationFinishedProcessor : IProcessor
    {
        private readonly IPlanetService m_planetService;

        public ExplorationFinishedProcessor(IPlanetService planetService)
        {
            m_planetService = planetService;
        }

        public MessageType Type { get; set; } = MessageType.ExplorationFinished;

        public async Task<RabbitResponse> ProcessAsync(string data)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<ExplorationResponse>(data);
                var (_, toUpdate) = await m_planetService.SearchPlanetAsync(new Pagination(), new Ordering(), new PlanetFilter
                {
                    SearchTerm = response.Planet.Id.ToString()
                });
                SetPlanetNewValues(toUpdate.First(), response);
                await m_planetService.UpdatePlanetAsync(toUpdate.First());
                return null;
            }
            catch
            {
                return new RabbitResponse
                {
                    IsSuccessful = false,
                    Message = string.Empty
                };
            }
        }

        private static void SetPlanetNewValues(Planet toUpdate, ExplorationResponse response)
        {
            toUpdate.GravityForce = response.Planet.GravityForce;
            toUpdate.OxygenPercentage = response.Planet.OxygenPercentage;
            toUpdate.TemperatureNight = response.Planet.TemperatureNight;
            toUpdate.TemperatureDay = response.Planet.TemperatureDay;
            toUpdate.WaterPercentage = response.Planet.WaterPercentage;
            toUpdate.GravityForce = response.Planet.GravityForce;
            toUpdate.PlanetRadius = response.Planet.PlanetRadius;
            toUpdate.PlanetSurfaceMagneticField = response.Planet.PlanetSurfaceMagneticField;
            toUpdate.AverageSolarWindVelocity = response.Planet.AverageSolarWindVelocity;
            toUpdate.SpinFrequency = response.Planet.SpinFrequency;
            toUpdate.PlanetStatus = response.Planet.PlanetStatus.Equals(PlanetStatus.Habitable) || response.Planet.PlanetStatus.Equals(PlanetStatus.Uninhabitable) ? response.Planet.PlanetStatus : PlanetStatus.Unexplored;
        }
    }
}