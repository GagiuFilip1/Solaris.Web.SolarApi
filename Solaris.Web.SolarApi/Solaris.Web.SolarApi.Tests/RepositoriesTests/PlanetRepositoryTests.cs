using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Filters;
using Solaris.Web.SolarApi.Infrastructure.Repositories.Implementations;
using Solaris.Web.SolarApi.Tests.Utils;
using Xunit;

namespace Solaris.Web.SolarApi.Tests.RepositoriesTests
{
    public class PlanetRepositoryTests : IClassFixture<DatabaseFixture>
    {
        public PlanetRepositoryTests(DatabaseFixture databaseFixture)
        {
            m_databaseFixture = databaseFixture;
            m_repository = new PlanetRepository(m_databaseFixture.DataContext);
        }

        private readonly DatabaseFixture m_databaseFixture;
        private readonly IPlanetRepository m_repository;

        [Fact]
        public async Task AddAndDeleteNewPlanet_Ok()
        {
            //Arrange
            var id = Guid.NewGuid();
            var system = new Planet
            {
                Id = id,
                Name = "Test"
            };

            //Act
            await m_repository.CreateAsync(system);

            //Assert
            var (_, planets) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = id.ToString()
            });
            Assert.Equal(id, planets.First().Id);
            Assert.Equal("Test", planets.First().Name);
            await m_repository.DeleteAsync(system);
            var (_, emptyResponse) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = id.ToString()
            });
            Assert.Empty(emptyResponse);
        }

        [Fact]
        public async void SearchWithFiltering_Ok()
        {
            //Arrange
            var firstPlanet = m_databaseFixture.DataContext.Planets.First(t => t.Id.Equals(DatabaseSeed.Planet1Id));

            //ACT
            var (_, idFiltered) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering(),
                new PlanetFilter
                {
                    SearchTerm = firstPlanet.Id.ToString()
                });

            var (_, nameFiltered) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering(),
                new PlanetFilter
                {
                    SearchTerm = "P"
                });


            //ASSERT
            Assert.Equal(firstPlanet.Id, idFiltered.First().Id);
            Assert.Equal(6, nameFiltered.Count);
        }

        [Fact]
        public async void SearchWithOrdering_Ok()
        {
            //ACT
            var (_, descResult) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering
                {
                    OrderBy = nameof(SolarSystem.Name),
                    OrderDirection = OrderDirection.Desc
                },
                new PlanetFilter());

            var (_, ascResult) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering
                {
                    OrderBy = nameof(SolarSystem.Name),
                    OrderDirection = OrderDirection.Asc
                },
                new PlanetFilter());

            //ASSERT
            Assert.Equal(descResult.Count, ascResult.Count);
            Assert.Equal(descResult.First().Id, ascResult.Last().Id);
            Assert.Equal(descResult.First().Name, m_databaseFixture.DataContext.Planets.First(t => t.Id.Equals(DatabaseSeed.Planet6Id)).Name);
            Assert.Equal(ascResult.First().Name, m_databaseFixture.DataContext.Planets.First(t => t.Id.Equals(DatabaseSeed.Planet1Id)).Name);
        }

        [Fact]
        public async void SearchWithPagination_Ok()
        {
            //ACT
            var (countNoOffset, systemsNoOffset) = await m_repository.SearchAsync(new Pagination
            {
                Take = 6,
                Offset = 0
            }, new Ordering(), new PlanetFilter());

            var (countWithOffset, systemsWithOffset) = await m_repository.SearchAsync(new Pagination
            {
                Take = 6,
                Offset = 5
            }, new Ordering(), new PlanetFilter());

            //ASSERT
            Assert.Equal(6, countNoOffset);
            Assert.Equal(6, systemsNoOffset.Count);
            Assert.Equal(6, countWithOffset);
            Assert.Equal(6, systemsWithOffset.Count);
            Assert.Single((IEnumerable) systemsWithOffset);
        }

        [Fact]
        public async void SimpleSearch_Ok()
        {
            //ACT
            var (count, planets) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter());

            //ASSERT
            Assert.Equal(6, count);
            Assert.Equal(DatabaseSeed.Planet1Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet1Id)).Id);
            Assert.Equal(DatabaseSeed.Planet2Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet2Id)).Id);
            Assert.Equal(DatabaseSeed.Planet3Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet3Id)).Id);
            Assert.Equal(DatabaseSeed.Planet4Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet4Id)).Id);
            Assert.Equal(DatabaseSeed.Planet5Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet5Id)).Id);
            Assert.Equal(DatabaseSeed.Planet6Id, planets.First(t => t.Id.Equals(DatabaseSeed.Planet6Id)).Id);
        }

        [Fact]
        public async Task UpdateNewPlanet_Ok()
        {
            //Arrange
            var id = Guid.NewGuid();
            var planet = new Planet
            {
                Id = id,
                Name = "Test",
                SolarSystemId = DatabaseSeed.SolarSystem1Id
            };

            //Act
            await m_repository.CreateAsync(planet);
            var (_, systems) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = id.ToString()
            });
            planet.Name = "Modified";
            await m_repository.UpdateAsync(planet);
            var (_, updatedResponse) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter
            {
                SearchTerm = id.ToString()
            });

            //Assert
            Assert.Equal(id, systems.First().Id);
            Assert.Equal("Modified", systems.First().Name);
            await m_repository.DeleteAsync(updatedResponse.First());
        }
    }
}