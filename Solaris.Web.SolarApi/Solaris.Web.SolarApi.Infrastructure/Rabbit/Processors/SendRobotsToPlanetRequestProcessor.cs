using System;
using System.Linq;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit.Processors
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class SendRobotsToPlanetRequestProcessor : IProcessor
    {
        private readonly IPlanetService m_planetService;

        public SendRobotsToPlanetRequestProcessor(IPlanetService planetService)
        {
            m_planetService = planetService;
        }

        public MessageType Type { get; set; } = MessageType.SendRobotsToPlanet;

        public async Task<RabbitResponse> ProcessAsync(string data)
        {
            try
            {
                var (_, response) = await m_planetService.SearchPlanetAsync(new Pagination(), new Ordering(), new PlanetFilter
                {
                    SearchTerm = data
                });
                var planet = response.First();
                planet.PlanetStatus = PlanetStatus.ExplorationInProcess;
                await m_planetService.UpdatePlanetAsync(response.First());
                return new RabbitResponse
                {
                    IsSuccessful = true,
                    Message = string.Empty
                };
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
    }
}