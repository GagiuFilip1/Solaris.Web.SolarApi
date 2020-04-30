using System;
using System.Collections.Generic;
using Solaris.Web.SolarApi.Core.Models.Entities;
using Solaris.Web.SolarApi.Core.Models.Helpers;

namespace Solaris.Web.SolarApi.Tests.Utils
{
    public static class DatabaseSeed
    {
        public static Guid SolarSystem1Id = Guid.NewGuid();
        public static Guid SolarSystem2Id = Guid.NewGuid();
        public static Guid SolarSystem3Id = Guid.NewGuid();

        public static Guid Planet1Id = Guid.NewGuid();
        public static Guid Planet2Id = Guid.NewGuid();
        public static Guid Planet3Id = Guid.NewGuid();
        public static Guid Planet4Id = Guid.NewGuid();
        public static Guid Planet5Id = Guid.NewGuid();
        public static Guid Planet6Id = Guid.NewGuid();

        public static List<SolarSystem> GetSolarSystems()
        {
            return new List<SolarSystem>
            {
                new SolarSystem
                {
                    Id = SolarSystem1Id,
                    Name = "S1",
                    SpacePosition = new SpaceCoordinates(1, 1, 1)
                },
                new SolarSystem
                {
                    Id = SolarSystem2Id,
                    Name = "S2",
                    SpacePosition = new SpaceCoordinates(2, 2, 2)
                },
                new SolarSystem
                {
                    Id = SolarSystem3Id,
                    Name = "S3",
                    SpacePosition = new SpaceCoordinates(3, 3, 3)
                }
            };
        }

        public static List<Planet> GetPlanets()
        {
            return new List<Planet>
            {
                new Planet
                {
                    Id = Planet1Id,
                    Name = "P1",
                    SolarSystemId = SolarSystem1Id
                },
                new Planet
                {
                    Id = Planet2Id,
                    Name = "P2",
                    SolarSystemId = SolarSystem1Id
                },
                new Planet
                {
                    Id = Planet3Id,
                    Name = "P3",
                    SolarSystemId = SolarSystem1Id
                },
                new Planet
                {
                    Id = Planet4Id,
                    Name = "P4",
                    SolarSystemId = SolarSystem2Id
                },
                new Planet
                {
                    Id = Planet5Id,
                    Name = "P5",
                    SolarSystemId = SolarSystem2Id
                },
                new Planet
                {
                    Id = Planet6Id,
                    Name = "P6",
                    SolarSystemId = SolarSystem3Id
                }
            };
        }
    }
}