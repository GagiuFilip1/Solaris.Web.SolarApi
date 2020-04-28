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

namespace Solaris.Web.SolarApi.Infrastructure.Services.Implementations
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository m_repository;
        private readonly ISolarSystemRepository m_solarSystemService;
        private readonly ILogger<PlanetService> m_logger;

        public PlanetService(ILogger<PlanetService> logger, IPlanetRepository repository, ISolarSystemRepository solarSystemService)
        {
            m_logger = logger;
            m_repository = repository;
            m_solarSystemService = solarSystemService;
        }

        public async Task CreatePlanetAsync(Planet planet)
        {
            try
            {
                var validationError = planet.Validate();
                if (validationError.Any())
                    throw new ValidationException($"A validation exception was raised while trying to create a planet : {JsonConvert.SerializeObject(validationError, Formatting.Indented)}");
                await EnsurePlanetSolarSystemExistsAsync(planet.SolarSystemId);
                await m_repository.CreateAsync(planet);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to create a planet with the properties : {JsonConvert.SerializeObject(planet, Formatting.Indented)}");
            }
        }

        public async Task UpdatePlanetAsync(Planet planet)
        {
            try
            {
                var validationError = planet.Validate();
                if (validationError.Any())
                    throw new ValidationException($"A validation exception was raised while trying to update a planet : {JsonConvert.SerializeObject(validationError, Formatting.Indented)}");
                await EnsurePlanetExistAsync(planet.Id);
                await EnsurePlanetSolarSystemExistsAsync(planet.SolarSystemId);
                await m_repository.UpdateAsync(planet);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to update a planet with the properties : {JsonConvert.SerializeObject(planet, Formatting.Indented)}");
            }
        }
        
        public async Task DeletePlanetAsync(Guid id)
        {
            try
            {
                var planet = await EnsurePlanetExistAsync(id);

                await m_repository.DeleteAsync(planet);
            }
            catch (ValidationException e)
            {
                m_logger.LogWarning(e, "A validation failed");
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to delete a Planet for id : {id}");
            }
        }

        public async Task<Tuple<int, List<Planet>>> SearchPlanetAsync(Pagination pagination, Ordering ordering, IFilter<Planet> filter)
        {
            try
            {
                return await m_repository.SearchAsync(pagination, ordering, filter);
            }
            catch (Exception e)
            {
                m_logger.LogCritical(e, $"Unexpected Exception while trying to search for Planets");
            }

            return default;
        }

        private async Task EnsurePlanetSolarSystemExistsAsync(Guid planetSolarSystemId)
        {
            var (_, searchResult) = await m_solarSystemService.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = planetSolarSystemId.ToString()
            });

            if (!searchResult.Any())
                throw new ValidationException("No Solar System was found for the specified Id");
        }
        
        private async Task<Planet> EnsurePlanetExistAsync(Guid id)
        {
            var (_, searchResult) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = id.ToString()
            });

            if (!searchResult.Any())
                throw new ValidationException("No Planet was found for the specified Id");
            return searchResult.First();
        }
    }
}