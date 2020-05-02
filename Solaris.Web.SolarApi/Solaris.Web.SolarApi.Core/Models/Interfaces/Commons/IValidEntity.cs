using System.Collections.Generic;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces.Commons
{
    public interface IValidEntity
    {
        List<string> Validate();
    }
}