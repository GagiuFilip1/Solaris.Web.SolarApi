using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Rabbit.Helpers.Responses;

namespace Solaris.Web.SolarApi.Core.Rabbit.Interfaces
{
    public interface IProcessor
    {
        public MessageType Type { get; set; }
        public Task<RabbitResponse> ProcessAsync(string data);
    }
}