using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Services.Implementations;
using Xunit;

namespace Solaris.Web.SolarApi.Tests.ServicesTests
{
    public class PlanetServiceTests
    {
        public PlanetServiceTests()
        {
            m_repositoryMock = new Mock<IPlanetRepository>();
            m_solarSystemRepositoryMock = new Mock<ISolarSystemRepository>();
            m_planetService = new PlanetService(new Mock<ILogger<PlanetService>>().Object, m_repositoryMock.Object, m_solarSystemRepositoryMock.Object);
        }

        private readonly Mock<IPlanetRepository> m_repositoryMock;
        private readonly Mock<ISolarSystemRepository> m_solarSystemRepositoryMock;
        private readonly PlanetService m_planetService;

        [Fact]
        public async void CreateWithInvalidData_Throws()
        {
            //Arrange
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_solarSystemRepositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            var invalidData = m_planetService.CreatePlanetAsync(new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                SolarSystemId = solarSystem.Id,
                SpinFrequency = -1,
                GravityForce = -1,
                PlanetRadius = -1,
                PlanetSurfaceMagneticField = -1,
                PlanetStatus = PlanetStatus.Uninhabitable
            });

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await invalidData);
            m_repositoryMock.Verify(t => t.CreateAsync(It.IsAny<Planet>()), Times.Never);
        }

        [Fact]
        public async void CreateWithRepositoryFail_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.CreateAsync(It.IsAny<Planet>())).ThrowsAsync(new Exception("Mocked"));
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_solarSystemRepositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            var repoFailed = m_planetService.CreatePlanetAsync(new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                SolarSystemId = solarSystem.Id,
                SpinFrequency = 8,
                GravityForce = 10,
                PlanetRadius = 18000000,
                PlanetSurfaceMagneticField = 8.9F
            });

            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await repoFailed);
        }

        [Fact]
        public async void CreateWithValidData_Ok()
        {
            //Arrange
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_solarSystemRepositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));
            //Act
            await m_planetService.CreatePlanetAsync(new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                SolarSystemId = solarSystem.Id,
                SpinFrequency = 8,
                GravityForce = 10,
                PlanetRadius = 18000000,
                PlanetSurfaceMagneticField = 8.9F
            });

            //Assert
            m_repositoryMock.Verify(t => t.CreateAsync(It.IsAny<Planet>()), Times.Once);
        }

        [Fact]
        public async void DeletePlanet_Ok()
        {
            //Arrange
            var planet = new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1"
            };
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<Planet>>()))
                .ReturnsAsync(new Tuple<int, List<Planet>>(1, new List<Planet> {planet}));

            //Act
            await m_planetService.DeletePlanetAsync(planet.Id);

            //Assert
            m_repositoryMock.Verify(t => t.DeleteAsync(It.IsAny<Planet>()), Times.Once);
        }

        [Fact]
        public async void DeletePlanetForInvalidPlanet_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<Planet>>()))
                .ReturnsAsync(new Tuple<int, List<Planet>>(0, new List<Planet>()));

            //Act
            var planetDoesNotExist = m_planetService.DeletePlanetAsync(Guid.NewGuid());

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await planetDoesNotExist);
        }

        [Fact]
        public async void SearchPlanet_Ok()
        {
            //Arrange
            var planet = new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1"
            };
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<Planet>>()))
                .ReturnsAsync(new Tuple<int, List<Planet>>(1, new List<Planet> {planet}));

            //Act
            var (count, planets) = await m_planetService.SearchPlanetAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = planet.Id.ToString()
            });

            //Assert
            m_repositoryMock.Verify(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<PlanetFilter>()), Times.Once);
            Assert.Equal(1, count);
            Assert.Equal(planet, planets.First());
        }

        [Fact]
        public async void UpdateInvalidPlanet_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<Planet>>()))
                .ReturnsAsync(new Tuple<int, List<Planet>>(0, new List<Planet>()));
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_solarSystemRepositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            var planetDoesNotExist = m_planetService.UpdatePlanetAsync(new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1"
            });

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await planetDoesNotExist);
        }

        [Fact]
        public async void UpdateWithValidData_Ok()
        {
            //Arrange
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            var planet = new Planet
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                SolarSystemId = solarSystem.Id,
                SpinFrequency = 8,
                GravityForce = 10,
                PlanetRadius = 18000000,
                PlanetSurfaceMagneticField = 8.9F
            };
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<Planet>>()))
                .ReturnsAsync(new Tuple<int, List<Planet>>(1, new List<Planet> {planet}));
            m_solarSystemRepositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            await m_planetService.UpdatePlanetAsync(planet);

            //Assert
            m_repositoryMock.Verify(t => t.UpdateAsync(It.IsAny<Planet>()), Times.Once);
        }
    }
}