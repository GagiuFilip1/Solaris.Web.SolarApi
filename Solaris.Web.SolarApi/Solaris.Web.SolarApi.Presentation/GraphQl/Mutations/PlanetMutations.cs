using System;
using System.ComponentModel.DataAnnotations;
using GraphQL;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.GraphQl.InputObjects.Planet;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Presentation.GraphQl.Mutations
{
    [RegistrationKind(Type = RegistrationType.Scoped, AsSelf = true)]
    public class PlanetMutations : ObjectGraphType
    {
        private const string CREATE_REQUEST_ENDPOINT = "create";
        private const string DELETE_REQUEST_ENDPOINT = "update";
        private const string UPDATE_REQUEST_ENDPOINT = "delete";
        private const string UPDATE_CREATE_ARGUMENT_NAME = "planet";
        private const string DELETE_ARGUMENT_NAME = "id";

        public PlanetMutations(IPlanetService service)
        {
            FieldAsync<ActionResponseType>(
                CREATE_REQUEST_ENDPOINT,
                "Creates a new Planet",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlanetCreateViewModel>>
                        {Name = UPDATE_CREATE_ARGUMENT_NAME, Description = "Planet Entity to be Created"}),
                async context =>
                {
                    var planet = context.GetArgument<Planet>(UPDATE_CREATE_ARGUMENT_NAME);

                    try
                    {
                        await service.CreatePlanetAsync(planet);
                    }
                    catch (ValidationException e)
                    {
                        context.Errors.Add(new ExecutionError(e.Message));
                        return new ActionResponse(false);
                    }
                    catch (Exception)
                    {
                        context.Errors.Add(new ExecutionError("Server Error"));
                        return new ActionResponse(false);
                    }

                    return new ActionResponse(true, planet.Id);
                });

            FieldAsync<ActionResponseType>(
                UPDATE_REQUEST_ENDPOINT,
                "Updates an existing Planet",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<PlanetUpdateViewModel>>
                        {Name = UPDATE_CREATE_ARGUMENT_NAME, Description = "Planet to be Updated"}),
                async context =>
                {
                    var planet = context.GetArgument<Planet>(UPDATE_CREATE_ARGUMENT_NAME);
                    try
                    {
                        await service.UpdatePlanetAsync(planet);
                    }
                    catch (ValidationException e)
                    {
                        context.Errors.Add(new ExecutionError(e.Message));
                        return new ActionResponse(false);
                    }
                    catch (Exception)
                    {
                        context.Errors.Add(new ExecutionError("Server Error"));
                        return new ActionResponse(false);
                    }

                    return new ActionResponse(true, planet.Id);
                });

            FieldAsync<ActionResponseType>(
                DELETE_REQUEST_ENDPOINT,
                "Removes an existing Planet",
                new QueryArguments(
                    new QueryArgument<GuidGraphType>
                        {Name = DELETE_ARGUMENT_NAME, Description = "Planet Id used to identify which planet will be deleted"}),
                async context =>
                {
                    var id = context.GetArgument<Guid>(DELETE_ARGUMENT_NAME);
                    try
                    {
                        await service.DeletePlanetAsync(id);
                    }
                    catch (ValidationException e)
                    {
                        context.Errors.Add(new ExecutionError(e.Message));
                        return new ActionResponse(false);
                    }
                    catch (Exception)
                    {
                        context.Errors.Add(new ExecutionError("Server Error"));
                        return new ActionResponse(false);
                    }

                    return new ActionResponse(true);
                });
        }
    }
}