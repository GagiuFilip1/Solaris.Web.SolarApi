using System;
using System.Collections.Generic;
using System.Numerics;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers;

namespace Solaris.Web.SolarApi.Core.Models
{
    public class SolarSystem : IIdentifier
    {
        public Guid Id { get; set; }
        public Vector3 SpacePosition { get; set; }
        public string Name { get; set; }
        public long DistanceToEarth { get; set; }
        public IEnumerable<Planet> Planets { get; set; }
    }
}