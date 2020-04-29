using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using GraphQL;
using GraphQL.Types;
using Solaris.Web.SolarApi.Core.GraphQl.Helpers;
using Solaris.Web.SolarApi.Core.GraphQl.InputObjects.SolarSystem;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Presentation.GraphQl.Mutations
{
    [RegistrationKind(Type = RegistrationType.Scoped, AsSelf = true)]
    public class SolarSystemMutations : ObjectGraphType
    {
        private const string CREATE_REQUEST_ENDPOINT = "create";
        private const string DELETE_REQUEST_ENDPOINT = "update";
        private const string UPDATE_REQUEST_ENDPOINT = "delete";
        private const string SOLAR_SYSTEM_ARGUMENT_NAME = "solarsystem";
        private const string DELETE_ARGUMENT_NAME = "id";

        public SolarSystemMutations(ISolarSystemService service)
        {
            FieldAsync<ActionResponseType>(
                CREATE_REQUEST_ENDPOINT,
                "Creates a new SolarSystem",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<SolarSystemCreateViewModel>> {Name = SOLAR_SYSTEM_ARGUMENT_NAME, Description = "SolarSystem Entity to be Created"}
                ),
                async context =>
                {
                    var solarSystem = context.GetArgument<SolarSystem>(SOLAR_SYSTEM_ARGUMENT_NAME);

                    try
                    {
                        await service.CreateSolarSystemAsync(solarSystem);
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

                    return new ActionResponse(true, solarSystem.Id);
                });

            FieldAsync<ActionResponseType>(
                UPDATE_REQUEST_ENDPOINT,
                "Updates an existing SolarSystem",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<SolarSystemUpdateViewModel>>
                        {Name = SOLAR_SYSTEM_ARGUMENT_NAME, Description = "SolarSystem to be Updated"}),
                async context =>
                {
                    var solarSystem = context.GetArgument<SolarSystem>(SOLAR_SYSTEM_ARGUMENT_NAME);
                    try
                    {
                        await service.UpdateSolarSystemAsync(solarSystem);
                    }
                    catch (ValidationException e)
                    {
                        context.Errors.Add(new ExecutionError(e.Message));
                        return new ActionResponse(false);
                    }
                    catch (Exception e)
                    {
                        context.Errors.Add(new ExecutionError(e.Message));
                        return new ActionResponse(false);
                    }

                    return new ActionResponse(true, solarSystem.Id);
                });

            FieldAsync<ActionResponseType>(
                DELETE_REQUEST_ENDPOINT,
                "Removes an existing SolarSystem",
                new QueryArguments(
                    new QueryArgument<GuidGraphType>
                        {Name = DELETE_ARGUMENT_NAME, Description = "SolarSystem Id used to identify which SolarSystem will be deleted"}),
                async context =>
                {
                    var id = context.GetArgument<Guid>(DELETE_ARGUMENT_NAME);
                    try
                    {
                        await service.DeleteSolarSystemAsync(id);
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