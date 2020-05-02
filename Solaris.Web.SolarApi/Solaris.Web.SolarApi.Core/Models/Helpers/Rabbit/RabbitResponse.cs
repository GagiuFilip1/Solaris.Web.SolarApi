namespace Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit
{
    public class RabbitResponse
    {
        public bool IsSuccessful { get; set; } = false;
        public string Message { get; set; }
    }
}