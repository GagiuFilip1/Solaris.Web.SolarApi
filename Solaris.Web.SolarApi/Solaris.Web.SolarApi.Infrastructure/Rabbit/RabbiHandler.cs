using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit
{
    [RegistrationKind(Type = RegistrationType.Scoped, AsSelf = true)]
    public class RabbiHandler
    {
        private readonly AppSettings m_appSettings;
        private readonly IEnumerable<IProcessor> m_processors;
        private readonly RabbitServer m_server;

        public RabbiHandler(IOptions<AppSettings> appSettings, RabbitServer server, IEnumerable<IProcessor> processors)
        {
            m_server = server;
            m_appSettings = appSettings.Value;
            m_processors = processors;
            SetProcessors();
            InitialiseRpcQueues();
        }

        private void InitialiseRpcQueues()
        {
            m_server.DeclareRpcQueue(new QueueSetup
            {
                Qos = 10,
                QueueName = m_appSettings.RabbitMqQueues.SolarApiQueue
            });
        }

        private void SetProcessors()
        {
            foreach (var processor in m_processors) m_server.Processors.TryAdd(processor.Type, processor.ProcessAsync);
        }
    }
}