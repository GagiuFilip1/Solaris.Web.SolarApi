using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit
{
    [RegistrationKind(Type = RegistrationType.Singleton, AsSelf = true)]
    public class RabbitConfigurator
    {
        private readonly AppSettings m_appSettings;
        private readonly RabbitServer m_server;

        public RabbitConfigurator(IOptions<AppSettings> appSettings, RabbitServer server, IEnumerable<IProcessor> processors)
        {
            m_server = server;
            m_appSettings = appSettings.Value;
            SetProcessors(processors);
        }

        private void InitialiseRpcQueues()
        {
            m_server.DeclareRpcQueue(new QueueSetup
            {
                Qos = 10,
                QueueName = m_appSettings.RabbitMqQueues.SolarApiQueue
            });
        }

        private void SetProcessors(IEnumerable<IProcessor> processors)
        {
            foreach (var processor in processors) m_server.Processors.TryAdd(processor.Type, processor.ProcessAsync);
        }
    }
}