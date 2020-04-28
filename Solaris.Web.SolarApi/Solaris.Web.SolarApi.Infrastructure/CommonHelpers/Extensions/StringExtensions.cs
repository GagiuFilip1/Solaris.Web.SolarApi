namespace Solaris.Web.SolarApi.Infrastructure.CommonHelpers.Extensions
{
    public static class StringExtensions
    {
        public static string ToMySqlLikeSyntax(this string value)
        {
            return $"%{value}%";
        }
    }
}