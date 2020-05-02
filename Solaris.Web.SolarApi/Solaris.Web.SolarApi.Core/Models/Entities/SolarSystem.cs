using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Solaris.Web.SolarApi.Core.Models.Helpers.Commons;
using Solaris.Web.SolarApi.Core.Models.Interfaces.Commons;

namespace Solaris.Web.SolarApi.Core.Models.Entities
{
    public class SolarSystem : IIdentifier, IValidEntity
    {
        [Required]
        [Column(TypeName = "varchar(256)")]
        public SpaceCoordinates SpacePosition { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Name { get; set; }

        [Required] public float DistanceToEarth { get; set; }

        public List<Planet> Planets { get; set; } = new List<Planet>();
        [Key] public Guid Id { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (Name.Any(char.IsLower))
                errors.Add("Name should contain only Uppercase Letter ,numbers and symbols");
            if (DistanceToEarth <= 4.369F)
                errors.Add("The distance to earth must be at least 4.369 light years away( Alpha Centauri )");
            return errors;
        }
    }
}