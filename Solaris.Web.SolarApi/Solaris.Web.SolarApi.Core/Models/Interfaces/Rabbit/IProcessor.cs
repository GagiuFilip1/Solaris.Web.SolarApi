using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit.Responses;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces.Rabbit
{
    public interface IProcessor
    {
        public MessageType Type { get; set; }
        public Task<RabbitResponse> ProcessAsync(string data);
    }
}