using System.Web;
using System.Web.Mvc;

namespace Intel.IT.Seci.Idam.Grs.MvcWeb
{
    #pragma warning disable 1591
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}