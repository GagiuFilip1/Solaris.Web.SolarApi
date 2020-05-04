using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Filters;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Services.Implementations;
using Xunit;

namespace Solaris.Web.SolarApi.Tests.ServicesTests
{
    public class SolarSystemServiceTests
    {
        public SolarSystemServiceTests()
        {
            m_repositoryMock = new Mock<ISolarSystemRepository>();
            m_solarSystemService = new SolarSystemService(m_repositoryMock.Object, new Mock<ILogger<SolarSystemService>>().Object);
        }

        private readonly Mock<ISolarSystemRepository> m_repositoryMock;
        private readonly SolarSystemService m_solarSystemService;

        [Fact]
        public async void CreateWithInvalidNameAndDistance_Throws()
        {
            //Act
            var invalidNameAndDistance = m_solarSystemService.CreateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "a-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            var invalidName = m_solarSystemService.CreateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "a-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            var invalidDistance = m_solarSystemService.CreateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.35F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await invalidNameAndDistance);
            await Assert.ThrowsAsync<ValidationException>(async () => await invalidName);
            await Assert.ThrowsAsync<ValidationException>(async () => await invalidDistance);
            m_repositoryMock.Verify(t => t.CreateAsync(It.IsAny<SolarSystem>()), Times.Never);
        }

        [Fact]
        public async void CreateWithRepositoryFail_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.CreateAsync(It.IsAny<SolarSystem>())).ThrowsAsync(new Exception("Mocked"));

            //Act
            var repoFailed = m_solarSystemService.CreateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            //Assert
            await Assert.ThrowsAsync<Exception>(async () => await repoFailed);
        }

        [Fact]
        public async void CreateWithValidData_Ok()
        {
            //Act
            await m_solarSystemService.CreateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            //Assert
            m_repositoryMock.Verify(t => t.CreateAsync(It.IsAny<SolarSystem>()), Times.Once);
        }

        [Fact]
        public async void DeleteSolarSystem_Ok()
        {
            //Arrange
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<SolarSystem>>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            await m_solarSystemService.DeleteSolarSystemAsync(solarSystem.Id);

            //Assert
            m_repositoryMock.Verify(t => t.DeleteAsync(It.IsAny<SolarSystem>()), Times.Once);
        }

        [Fact]
        public async void DeleteSolarSystemForInvalidSolarSystem_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<SolarSystem>>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(0, new List<SolarSystem>()));

            //Act
            var solarSystemDoesNotExist = m_solarSystemService.DeleteSolarSystemAsync(Guid.NewGuid());

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await solarSystemDoesNotExist);
        }

        [Fact]
        public async void SearchSolarSystem_Ok()
        {
            //Arrange
            var solarSystem = new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            };
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<SolarSystem>>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            var (count, solarSystems) = await m_solarSystemService.SearchSolarSystemAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = solarSystem.Id.ToString()
            });

            //Assert
            m_repositoryMock.Verify(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<SolarSystemFilter>()), Times.Once);
            Assert.Equal(1, count);
            Assert.Equal(solarSystem, solarSystems.First());
        }

        [Fact]
        public async void UpdateInvalidSolarSystem_Throws()
        {
            //Arrange
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<SolarSystem>>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(0, new List<SolarSystem>()));

            //Act
            var solarSystemDoesNotExist = m_solarSystemService.UpdateSolarSystemAsync(new SolarSystem
            {
                Id = Guid.NewGuid(),
                Name = "A-1",
                DistanceToEarth = 4.38F,
                SpacePosition = new SpaceCoordinates(12323.54F, 53432.24F, 23131.01F)
            });

            //Assert
            await Assert.ThrowsAsync<ValidationException>(async () => await solarSystemDoesNotExist);
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
            m_repositoryMock.Setup(t => t.SearchAsync(It.IsAny<Pagination>(), It.IsAny<Ordering>(), It.IsAny<IFilter<SolarSystem>>()))
                .ReturnsAsync(new Tuple<int, List<SolarSystem>>(1, new List<SolarSystem> {solarSystem}));

            //Act
            await m_solarSystemService.UpdateSolarSystemAsync(solarSystem);

            //Assert
            m_repositoryMock.Verify(t => t.UpdateAsync(It.IsAny<SolarSystem>()), Times.Once);
        }
    }
}