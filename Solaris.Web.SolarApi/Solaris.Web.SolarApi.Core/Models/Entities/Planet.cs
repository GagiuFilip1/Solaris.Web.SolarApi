﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Solaris.Web.SolarApi.Core.Enums;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.Models.Entities
{
    public class Planet : IIdentifier, IValidEntity
    {
        [Key] public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
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

        [Column(TypeName = "text")] public string Description { get; set; }

        [Required] public Guid SolarSystemId { get; set; }

        public SolarSystem SolarSystem { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (SpinFrequency <= 0)
                errors.Add("A planet can't have a spin Frequency <= 0");
            if (GravityForce <= 0)
                errors.Add("A planet can't have a Gravity Force <= 0 G's");
            if (PlanetRadius <= 0)
                errors.Add("A planet can't have a Radius < 0");
            if (PlanetSurfaceMagneticField <= 0)
                errors.Add("A planet can't have a Magnetic Field <= 0 Teslas");
            if (SolarWindVelocity < 0)
                errors.Add("A planet can't have a Solar Wind Velocity < 0 KM/S");
            return errors;
        }
    }
}