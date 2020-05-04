using Solaris.Web.SolarApi.Core.Models.Entities;

namespace Solaris.Web.SolarApi.Core.Rabbit.Helpers.Responses
{
    public class ExplorationResponse
    {
        public bool IsSuccessful { get; set; }
        public Planet Planet { get; set; }
    }
}