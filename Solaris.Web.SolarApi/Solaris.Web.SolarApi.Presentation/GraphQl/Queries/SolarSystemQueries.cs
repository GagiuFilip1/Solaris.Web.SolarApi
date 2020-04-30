using System;
using GraphQL;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Presentation.GraphQl.Queries
{
    [RegistrationKind(Type = RegistrationType.Scoped, AsSelf = true)]
    public class SolarSystemQueries : ObjectGraphType
    {
        private const string SEARCH_REQUEST_ENDPOINT = "search";
        private const string PAGINATION_ARGUMENT_NAME = "pagination";
        private const string ORDERING_ARGUMENT_NAME = "ordering";
        private const string FILTERING_ARGUMENT_NAME = "filtering";

        public SolarSystemQueries(ISolarSystemService solarSystemService)
        {
            FieldAsync<ListSolarSystemsQueryModelType>(
                SEARCH_REQUEST_ENDPOINT,
                "Returns a paginated list of SolarSystems",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<PagedRequestType>> {Name = PAGINATION_ARGUMENT_NAME, Description = PagedRequestType.Description},
                    new QueryArgument<NonNullGraphType<OrderedRequestType>> {Name = ORDERING_ARGUMENT_NAME, Description = OrderedRequestType.Description},
                    new QueryArgument<NonNullGraphType<FilteredRequestType<SolarSystem>>> {Name = FILTERING_ARGUMENT_NAME, Description = FilteredRequestType<SolarSystem>.Description}
                ),
                async context =>
                {
                    var pagination = context.GetArgument<Pagination>(PAGINATION_ARGUMENT_NAME);
                    var ordering = context.GetArgument<Ordering>(ORDERING_ARGUMENT_NAME);
                    var filtering = context.GetArgument<SolarSystemFilter>(FILTERING_ARGUMENT_NAME);

                    var (totalCount, items) = await solarSystemService.SearchSolarSystemAsync(pagination, ordering, filtering);
                    try
                    {
                        return new ListResponse<SolarSystem>
                        {
                            TotalCount = totalCount,
                            Items = items
                        };
                    }
                    catch (Exception e)
                    {
                        context.Errors.Add(new ExecutionError("Server Error"));
                        return null;
                    }
                }
            );
        }
    }
}