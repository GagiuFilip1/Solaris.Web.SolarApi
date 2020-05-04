using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Filters.Implementations;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Rabbit.Helpers.Responses;
using Solaris.Web.SolarApi.Core.Rabbit.Interfaces;
using Solaris.Web.SolarApi.Core.Rabbit.Models;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit.Processors
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class SendRobotsToPlanetRequestProcessor : IProcessor
    {
        private readonly AppSettings m_appSettings;
        private readonly IRabbitHandler m_handler;
        private readonly IPlanetService m_planetService;

        public SendRobotsToPlanetRequestProcessor(IPlanetService planetService, IRabbitHandler handler, IOptions<AppSettings> appSettings)
        {
            m_planetService = planetService;
            m_handler = handler;
            m_appSettings = appSettings.Value;
        }

        public MessageType Type { get; set; } = MessageType.SendRobotsToPlanet;

        public async Task<RabbitResponse> ProcessAsync(string data)
        {
            try
            {
                var request = JObject.Parse(data);
                var (_, response) = await m_planetService.SearchPlanetAsync(new Pagination(), new Ordering(), new PlanetFilter
                {
                    SearchTerm = request["PlanetId"].ToString()
                });
                var planet = response.First();
                planet.PlanetStatus = PlanetStatus.ExplorationInProcess;
                await m_planetService.UpdatePlanetAsync(response.First());
                SendExplorationRequest(planet, request);
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

        private void SendExplorationRequest(Planet planet, JObject request)
        {
            m_handler.Publish(new PublishOptions
            {
                Message = JsonConvert.SerializeObject(new
                {
                    Planet = planet,
                    Robots = request["Robots"]
                }),
                Headers = new Dictionary<string, object>
                {
                    {nameof(MessageType), nameof(MessageType.StartExplorationProcess)}
                },
                TargetQueue = m_appSettings.RabbitMqQueues.ExplorationQueue
            });
        }
    }
}