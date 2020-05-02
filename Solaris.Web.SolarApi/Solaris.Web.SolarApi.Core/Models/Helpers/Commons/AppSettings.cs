using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit;

namespace Solaris.Web.SolarApi.Core.Models.Helpers.Commons
{
    public class AppSettings
    {
        public RabbitMqSettings RabbitMq { get; set; }

        public RabbitMqQueues RabbitMqQueues { get; set; }
    }
}