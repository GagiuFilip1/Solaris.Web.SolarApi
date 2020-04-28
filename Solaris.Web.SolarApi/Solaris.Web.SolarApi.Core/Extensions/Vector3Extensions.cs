using System.Numerics;

namespace Solaris.Web.SolarApi.Core.Extensions
{
    public static class Vector3Extensions
    {
        private const string SEPARATOR = "-";

        public static string ToDbValue(this Vector3 value)
        {
            return $"{value.X}{SEPARATOR}{value.Y}{SEPARATOR}{value.Z}";
        }

        public static Vector3 FromDbValue(this Vector3 _, string value)
        {
            var coordinates = value.Split(SEPARATOR);
            return new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
        }
    }
}