namespace Solaris.Web.SolarApi.Core.Rabbit.Helpers.Responses
{
    public class RabbitResponse
    {
        public bool IsSuccessful { get; set; } = false;
        public string Message { get; set; }
    }
}