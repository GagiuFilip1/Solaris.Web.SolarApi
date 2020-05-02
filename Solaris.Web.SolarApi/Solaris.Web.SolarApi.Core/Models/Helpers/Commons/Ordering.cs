namespace Solaris.Web.SolarApi.Core.Models.Helpers.Commons
{
    public class Ordering
    {
        private string m_orderBy = "Id";

        public string OrderBy
        {
            get => m_orderBy;
            set => m_orderBy = value?.ToLower();
        }

        public OrderDirection OrderDirection { get; set; } = OrderDirection.Asc;
    }

    public enum OrderDirection
    {
        Asc = 0,
        Desc = 1
    }
}