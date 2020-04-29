using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.GraphQl.Root;
using Solaris.Web.SolarApi.Infrastructure.Ioc;
using Solaris.Web.SolarApi.Presentation.GraphQl.Mutations;
using Solaris.Web.SolarApi.Presentation.GraphQl.Queries;

namespace Solaris.Web.SolarApi.Presentation.GraphQl.Schemas
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class SolarSystemSchema : ISchemaGroup
    {
        private const string FIELD_NAME = "solarsystems";

        public void SetGroup(RootQuery query)
        {
            query.Field<SolarSystemQueries>(
                FIELD_NAME,
                resolve: context => new { });
        }

        public void SetGroup(RootMutation mutation)
        {
            mutation.Field<SolarSystemMutations>(
                FIELD_NAME,
                resolve: context => new { });
        }
    }
}