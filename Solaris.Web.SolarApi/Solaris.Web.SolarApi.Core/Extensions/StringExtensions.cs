namespace Solaris.Web.SolarApi.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToMySqlLikeSyntax(this string value)
        {
            return $"%{value}%";
        }
    }
}