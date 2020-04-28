using System;
using System.Linq;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Filters;
using Solaris.Web.SolarApi.Core.Repositories.Implementations;
using Solaris.Web.SolarApi.Core.Repositories.Interfaces;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Models;
using Solaris.Web.SolarApi.Tests.Utils;
using Xunit;

namespace Solaris.Web.SolarApi.Tests.RepositoryTests
{
    public class PlanetRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture m_databaseFixture;
        private readonly IPlanetRepository m_repository;

        public PlanetRepositoryTests(DatabaseFixture databaseFixture)
        {
            m_databaseFixture = databaseFixture;
            m_repository = new PlanetRepository(m_databaseFixture.DataContext);
        }

        [Fact]
        public async void SimpleSearch_Ok()
        {
            //ACT
            var (count, planets) = await m_repository.SearchAsync(new Pagination(), new Ordering(), new PlanetFilter());

            //ASSERT
            Assert.Equal(6, count);
            Assert.Equal(DataBaseSeed.Planet1Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet1Id)).Id);
            Assert.Equal(DataBaseSeed.Planet2Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet2Id)).Id);
            Assert.Equal(DataBaseSeed.Planet3Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet3Id)).Id);
            Assert.Equal(DataBaseSeed.Planet4Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet4Id)).Id);
            Assert.Equal(DataBaseSeed.Planet5Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet5Id)).Id);
            Assert.Equal(DataBaseSeed.Planet6Id, planets.First(t => t.Id.Equals(DataBaseSeed.Planet6Id)).Id);
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
            Assert.Single(systemsWithOffset);
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
            Assert.Equal(descResult.First().Name, m_databaseFixture.DataContext.Planets.First(t => t.Id.Equals(DataBaseSeed.Planet6Id)).Name);
            Assert.Equal(ascResult.First().Name, m_databaseFixture.DataContext.Planets.First(t => t.Id.Equals(DataBaseSeed.Planet1Id)).Name);
        }
    }
}