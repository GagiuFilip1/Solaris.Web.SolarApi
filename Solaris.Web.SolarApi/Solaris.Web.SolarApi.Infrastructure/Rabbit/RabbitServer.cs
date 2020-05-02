using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Rabbit
{
    [RegistrationKind(Type = RegistrationType.Singleton, AsSelf = true)]
    public class RabbitServer
    {
        private readonly AppSettings m_appSettings;

        private readonly ILogger<RabbitServer> m_logger;

        public RabbitServer(ILogger<RabbitServer> logger, IOptions<AppSettings> appSettings)
        {
            m_logger = logger;
            m_appSettings = appSettings.Value;
        }

        public Dictionary<MessageType, Func<string, Task<RabbitResponse>>> Processors { get; } = new Dictionary<MessageType, Func<string, Task<RabbitResponse>>>();

        public void DeclareRpcQueue(QueueSetup setup)
        {
            InitialiseQueue(setup.QueueName, setup.Qos, out var consumer, out var channel);
            consumer.Received += async (model, eventArgs) =>
            {
                try
                {
                    var headers = eventArgs.BasicProperties.Headers.ToDictionary(
                        t => t.Key,
                        t => Encoding.UTF8.GetString(t.Value as byte[]));
                    var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    var basicProperties = eventArgs.BasicProperties;
                    var properties = channel.CreateBasicProperties();
                    properties.CorrelationId = basicProperties.CorrelationId;

                    try
                    {
                        Enum.TryParse(headers[nameof(MessageType)], out MessageType type);
                        var data = await Processors[type].Invoke(body);
                        PublishAndAcknowledge(channel, basicProperties, properties, data, eventArgs);
                    }
                    catch (Exception e)
                    {
                        m_logger.LogError(e, $"Could not finish a remote request : {JsonConvert.SerializeObject(headers)}");
                        PublishAndAcknowledge(channel, basicProperties, properties, new RabbitResponse(), eventArgs);
                    }
                }
                catch (Exception e)
                {
                    m_logger.LogCritical(e, $"Could not extract request information for {setup.QueueName}");
                }
            };
        }

        private static void PublishAndAcknowledge(IModel channel, IBasicProperties basicProperties, IBasicProperties properties, RabbitResponse data, BasicDeliverEventArgs eventArgs)
        {
            channel.BasicPublish(string.Empty, basicProperties.ReplyTo, properties, CreateResponse(data));
            channel.BasicAck(eventArgs.DeliveryTag, false);
        }

        private static byte[] CreateResponse(RabbitResponse data)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        }

        private void InitialiseQueue(string queue, ushort qos, out EventingBasicConsumer consumer, out IModel channel)
        {
            var factory = new ConnectionFactory
            {
                HostName = m_appSettings.RabbitMq.Host,
                Port = m_appSettings.RabbitMq.Port,
                UserName = m_appSettings.RabbitMq.Username,
                Password = m_appSettings.RabbitMq.Password
            };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue, false, false, false, null);
            channel.BasicQos(0, qos, false);
            consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue, false, consumer);
        }
    }
}