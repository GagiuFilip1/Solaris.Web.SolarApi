using Solaris.Web.SolarApi.Core.GraphQl.Root;

namespace Solaris.Web.SolarApi.Core.GraphQl.Helpers
{
    public interface ISchemaGroup
    {
        void SetGroup(RootQuery query);
        void SetGroup(RootMutation mutation);
    }
}