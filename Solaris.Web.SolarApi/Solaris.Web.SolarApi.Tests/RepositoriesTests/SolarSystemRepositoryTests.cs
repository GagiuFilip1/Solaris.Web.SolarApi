using System;
using System.Linq;
using System.Threading.Tasks;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Filters.Implementations;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.Repositories.Implementations;
using Solaris.Web.SolarApi.Tests.Utils;
using Xunit;

namespace Solaris.Web.SolarApi.Tests.RepositoriesTests
{
    public class SolarSystemRepositoryTests : IClassFixture<DatabaseFixture>
    {
        public SolarSystemRepositoryTests(DatabaseFixture databaseFixture)
        {
            m_databaseFixture = databaseFixture;
            m_repository = new SolarSystemRepository(m_databaseFixture.DataContext);
        }

        private readonly DatabaseFixture m_databaseFixture;
        private readonly ISolarSystemRepository m_repository;

        [Fact]
        public async Task AddAndDeleteNewSolarSystem_Ok()
        {
            //Arrange
            var id = Guid.NewGuid();
            var system = new SolarSystem
            {
                Id = id,
                Name = "Test"
            };

            //Act
            await m_repository.CreateAsync(system);

            //Assert
            var (_, systems) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = id.ToString()
            });
            Assert.Equal(id, systems.First().Id);
            Assert.Equal("Test", systems.First().Name);
            await m_repository.DeleteAsync(system);
            var (_, emptyResponse) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = id.ToString()
            });
            Assert.Empty(emptyResponse);
        }

        [Fact]
        public async void SearchWithFiltering_Ok()
        {
            //Arrange
            var firstSolarSystem = m_databaseFixture.DataContext.SolarSystems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem1Id));
            var thirdSolarSystem = m_databaseFixture.DataContext.SolarSystems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem3Id));

            //ACT
            var (_, idFiltered) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering(),
                new SolarSystemFilter
                {
                    SearchTerm = firstSolarSystem.Id.ToString()
                });

            var (_, nameFiltered) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering(),
                new SolarSystemFilter
                {
                    SearchTerm = "S"
                });

            var (_, positionFiltered) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering(),
                new SolarSystemFilter
                {
                    SearchTerm = thirdSolarSystem.SpacePosition.ToString()
                });

            //ASSERT
            Assert.Equal(firstSolarSystem.Id, idFiltered.First().Id);
            Assert.Equal(3, nameFiltered.Count);
            Assert.Equal(thirdSolarSystem.SpacePosition, positionFiltered.First().SpacePosition);
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
                new SolarSystemFilter());

            var (_, ascResult) = await m_repository.SearchAsync(
                new Pagination(),
                new Ordering
                {
                    OrderBy = nameof(SolarSystem.Name),
                    OrderDirection = OrderDirection.Asc
                },
                new SolarSystemFilter());

            //ASSERT
            Assert.Equal(descResult.Count, ascResult.Count);
            Assert.Equal(descResult.First().Id, ascResult.Last().Id);
            Assert.Equal(descResult.First().Name, m_databaseFixture.DataContext.SolarSystems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem3Id)).Name);
            Assert.Equal(ascResult.First().Name, m_databaseFixture.DataContext.SolarSystems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem1Id)).Name);
        }

        [Fact]
        public async void SearchWithPagination_Ok()
        {
            //ACT
            var (countNoOffset, systemsNoOffset) = await m_repository.SearchAsync(new Pagination
            {
                Take = 3,
                Offset = 0
            }, new Ordering(), new SolarSystemFilter());

            var (countWithOffset, systemsWithOffset) = await m_repository.SearchAsync(new Pagination
            {
                Take = 3,
                Offset = 2
            }, new Ordering(), new SolarSystemFilter());

            //ASSERT
            Assert.Equal(3, countNoOffset);
            Assert.Equal(3, systemsNoOffset.Count);

            Assert.Equal(3, countWithOffset);
            Assert.Single(systemsWithOffset);
        }

        [Fact]
        public async void SimpleSearch_Ok()
        {
            //ACT
            var (count, systems) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter());

            //ASSERT
            Assert.Equal(3, count);
            Assert.Equal(DatabaseSeed.SolarSystem1Id, systems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem1Id)).Id);
            Assert.Equal(DatabaseSeed.SolarSystem2Id, systems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem2Id)).Id);
            Assert.Equal(DatabaseSeed.SolarSystem3Id, systems.First(t => t.Id.Equals(DatabaseSeed.SolarSystem3Id)).Id);
        }

        [Fact]
        public async Task UpdateNewSolarSystem_Ok()
        {
            //Arrange
            var id = Guid.NewGuid();
            var system = new SolarSystem
            {
                Id = id,
                Name = "Test"
            };

            //Act
            await m_repository.CreateAsync(system);
            system.Name = "Modified";
            await m_repository.UpdateAsync(system);
            var (_, updatedResponse) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new SolarSystemFilter
            {
                SearchTerm = id.ToString()
            });

            //Assert
            Assert.Equal(id, updatedResponse.First().Id);
            Assert.Equal("Modified", updatedResponse.First().Name);
            await m_repository.DeleteAsync(system);
        }
    }
}