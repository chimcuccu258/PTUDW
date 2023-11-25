using System.Web;
using System.Web.Mvc;

namespace QLVinpearl_63130803
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
