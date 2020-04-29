namespace Solaris.Web.SolarApi.Core.Models.Helpers
{
    public class Pagination
    {
        public int Take { get; set; } = 100;

        public int Offset { get; set; }
    }
}