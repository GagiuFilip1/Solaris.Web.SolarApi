using System.Collections.Generic;

namespace Solaris.Web.SolarApi.Core.Models.Interfaces
{
    public interface IValidEntity
    {
        List<string> Validate();
    }
}