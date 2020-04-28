namespace Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Models
{
    public class Pagination
    {
        public int Take { get; set; } = 100;

        public int Offset { get; set; }
    }
}