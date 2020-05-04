using System.Web;
using System.Web.Mvc;

namespace CRIntranet
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Filters.HSTSAttribute(
                System.TimeSpan.FromMinutes(5), true, false));
            filters.Add(new HandleErrorAttribute());
        }
    }
}
