using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;

namespace Solaris.Web.SolarApi.Core.Models.Entities
{
    public class Planet : IIdentifier, IValidEntity
    {
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        public float TemperatureNight { get; set; }

        public float TemperatureDay { get; set; }

        public float WaterPercentage { get; set; }

        public float OxygenPercentage { get; set; }

        public float GravityForce { get; set; }

        public float PlanetRadius { get; set; }

        public float PlanetSurfaceMagneticField { get; set; }

        public float AverageSolarWindVelocity { get; set; }

        public float SpinFrequency { get; set; }

        public PlanetStatus PlanetStatus { get; set; }

        [Column(TypeName = "text")] public string Description { get; set; }

        [Column(TypeName = "varchar(2048)")] public Uri ImageUrl { get; set; }

        [Required] public Guid SolarSystemId { get; set; }

        [JsonIgnore] [IgnoreDataMember] public SolarSystem SolarSystem { get; set; }
        [Key] public Guid Id { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (!PlanetStatus.Equals(PlanetStatus.Habitable) && !PlanetStatus.Equals(PlanetStatus.Uninhabitable))
                return errors;

            if (SpinFrequency < 0)
                errors.Add("A planet can't have a spin Frequency < 0");
            if (GravityForce < 0)
                errors.Add("A planet can't have a Gravity Force < 0 m/s^2");
            if (PlanetRadius < 0)
                errors.Add("A planet can't have a Radius < 0");
            if (PlanetSurfaceMagneticField < 0)
                errors.Add("A planet can't have a Magnetic Field <= 0 A/m");
            if (AverageSolarWindVelocity < 0)
                errors.Add("A planet can't have a Solar Wind Velocity < 0 KM/S");

            return errors;
        }
    }
}