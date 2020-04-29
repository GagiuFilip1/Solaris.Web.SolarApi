using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solaris.Web.SolarApi.Core.Models;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Interfaces;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Core.Services.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Ioc;

namespace Solaris.Web.SolarApi.Infrastructure.Services.Implementations
{
    [RegistrationKind(Type = RegistrationType.Scoped)]
    public class SolarSystemService : ISolarSystemService
    {
        private readonly ISolarSystemRepository m_repository;
        private readonly ILogger<SolarSystemService> m_logger;

        public SolarSystemService(ISolarSystemRepository repository, ILogger<SolarSystemService> logger)
        {
            m_repository = repository;
            m_logger = logger;
        }

        public async Task CreateSolarSystemAsync(SolarSystem solarSystem)
        {
            try
            {
                var validationError = solarSystem.Validate();
                if (validationError.Any())
                    throw new ValidationException($"A validation exception was raised while trying to create a solar system : {JsonConvert.SerializeObject(validationError, Formatting.Indented)}");

                await m_repository.CreateAsync(solarSystem);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a solar system with the properties : {JsonConvert.SerializeObject(solarSystem, Formatting.Indented)}");
                throw;
            }
        }

        public async Task UpdateSolarSystemAsync(SolarSystem solarSystem)
        {
            try
            {
                var validationError = solarSystem.Validate();
                if (validationError.Any())
                    throw new ValidationException($"A validation exception was raised while trying to update a solar system : {JsonConvert.SerializeObject(validationError, Formatting.Indented)}");
                await EnsureSolarSystemExistAsync(solarSystem.Id);
                await m_repository.UpdateAsync(solarSystem);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a solar system with the properties : {JsonConvert.SerializeObject(solarSystem, Formatting.Indented)}");
                throw;
            }
        }

        public async Task DeleteSolarSystemAsync(Guid id)
        {
            try
            {
                var solarSystem = await EnsureSolarSystemExistAsync(id);

                await m_repository.DeleteAsync(solarSystem);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
                throw;
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a solar system for id : {id}");
                throw;
            }
        }

        public async Task<Tuple<int, List<SolarSystem>>> SearchSolarSystemAsync(Pagination pagination, Ordering ordering, IFilter<SolarSystem> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, ordering, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to search for Solar Systems");
                throw;
            }
        }
        
        private async Task<SolarSystem> EnsureSolarSystemExistAsync(Guid id)
        {
            var (_, searchResult) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = id.ToString()
            });

            if (!searchResult.Any())
                throw new ValidationException("No Solar System was found for the specified Id");
            return searchResult.First();
        }
    }
}