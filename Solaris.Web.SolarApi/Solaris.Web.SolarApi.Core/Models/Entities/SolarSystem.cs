using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using Solaris.Web.SolarApi.Core.Models.Interfaces;

namespace Solaris.Web.SolarApi.Core.Models.Entities
{
    public class SolarSystem : IIdentifier, IValidEntity
    {
        [Key] public Guid Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public Vector3 SpacePosition { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }

        [Required] public long DistanceToEarth { get; set; }

        public List<Planet> Planets { get; set; } = new List<Planet>();

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (Name.Any(char.IsLower))
                errors.Add("Name should contain only Uppercase Letter ,numbers and symbols");
            if (DistanceToEarth < 4.37)
                errors.Add("The distance to earth must be at least 4.37 light years away( Alpha Centauri )");
            return errors;
        }
    }
}