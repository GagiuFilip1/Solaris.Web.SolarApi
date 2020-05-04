using System;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Enums;

namespace Solaris.Web.SolarApi.Core.Rabbit.Models
{
    public class ListenOptions
    {
        public string TargetQueue { get; set; }
        public Func<string, Task> RequestParser { get; set; }
        public MessageType MessageType { get; set; }
        public ushort Qos { get; set; }
    }
}