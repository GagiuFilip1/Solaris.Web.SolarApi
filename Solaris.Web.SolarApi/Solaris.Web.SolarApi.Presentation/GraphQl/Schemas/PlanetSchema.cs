using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.GraphQl.Root;
using Solaris.Web.SolarApi.Presentation.GraphQl.Mutations;
using Solaris.Web.SolarApi.Presentation.GraphQl.Queries;

namespace Solaris.Web.SolarApi.Presentation.GraphQl.Schemas
{
    public class PlanetSchema : ISchemaGroup
    {
        private const string FIELD_NAME = "planets";
        public void SetGroup(RootQuery query)
        {
            query.Field<PlanetQueries>(
                FIELD_NAME,
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<PlanetMutations>(
                FIELD_NAME,
                resolve: context => new { });
        }
    }
}