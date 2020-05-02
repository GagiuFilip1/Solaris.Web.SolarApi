namespace Solaris.Web.SolarApi.Core.Models.Helpers.Rabbit
{
    public class QueueSetup
    {
        public string QueueName { get; set; }
        public ushort Qos { get; set; }
    }
}