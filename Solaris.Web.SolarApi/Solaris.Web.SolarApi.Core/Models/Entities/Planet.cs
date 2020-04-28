using System;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Interfaces;

namespace Solaris.Web.SolarApi.Core.Models.Entities
{
    public class Planet : IIdentifier
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float TemperatureNight { get; set; }
        public float TemperatureDay { get; set; }
        public float WaterPercentage { get; set; }
        public float OxygenPercentage { get; set; }
        public float GravityForce { get; set; }
        public double PlanetRadius { get; set; }
        public double PlanetSurfaceMagneticField { get; set; }
        public double SolarWindVelocity { get; set; }
        public float SpinFrequency { get; set; }
        public PlanetStatus PlanetStatus { get; set; }
        public string Description { get; set; }
        public Guid SolarSystemId { get; set; }
        public SolarSystem SolarSystem { get; set; }
    }
}