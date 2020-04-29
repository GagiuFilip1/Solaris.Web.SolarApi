using System;

namespace Solaris.Web.SolarApi.Core.Models.Helpers
{
    public class SpaceCoordinates
    {
        public SpaceCoordinates()
        {
        }

        public SpaceCoordinates(float xCoordonate, float yCoordonate, float zCoordonate)
        {
            XCoordonate = xCoordonate;
            YCoordonate = yCoordonate;
            ZCoordonate = zCoordonate;
        }

        public float XCoordonate { get; set; }
        public float YCoordonate { get; set; }
        public float ZCoordonate { get; set; }

        public override string ToString()
        {
            return $"{XCoordonate}-{YCoordonate}-{ZCoordonate}";
        }

        public static SpaceCoordinates FromString(string value)
        {
            try
            {
                var values = value.Split("-");
                return new SpaceCoordinates
                {
                    XCoordonate = float.Parse(values[0]),
                    YCoordonate = float.Parse(values[0]),
                    ZCoordonate = float.Parse(values[0])
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}