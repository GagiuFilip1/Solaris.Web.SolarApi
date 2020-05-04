using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Rabbit.Helpers.Responses;
using Solaris.Web.SolarApi.Core.Rabbit.Helpers.Setup;
using Solaris.Web.SolarApi.Core.Rabbit.Models;

namespace Solaris.Web.SolarApi.Core.Rabbit.Interfaces
{
    public interface IRabbitHandler
    {
        public Dictionary<MessageType, Func<string, Task<RabbitResponse>>> Processors { get; }
        T PublishRpc<T>(PublishOptions options);
        void PublishRpc(PublishOptions options);
        void Publish(PublishOptions options);
        void ListenQueueAsync(ListenOptions options);
        void DeclareRpcQueue(QueueSetup setup);
        void DeclareQueue(QueueSetup queueSetup);
    }
}