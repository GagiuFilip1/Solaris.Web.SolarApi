using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit.Responses;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit.Processors
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class PlanetCheckRequestProcessor : IProcessor
    {
        private readonly IPlanetService m_planetService;

        public PlanetCheckRequestProcessor(IPlanetService planetService)
        {
            m_planetService = planetService;
        }

        public MessageType Type { get; set; } = MessageType.CheckPlanet;

        public async Task<RabbitResponse> ProcessAsync(string data)
        {
            try
            {
                var (_, response) = await m_planetService.SearchPlanetAsync(new Pagination(), new Ordering(), new PlanetFilter
                {
                    SearchTerm = data
                });

                return new RabbitResponse
                {
                    IsSuccessful = response.Count == 1,
                    Message = response.Count == 1 ? JsonConvert.SerializeObject(response.First()) : string.Empty
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