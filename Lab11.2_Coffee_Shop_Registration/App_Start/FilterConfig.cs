using System.Web;
using System.Web.Mvc;

namespace Lab11._2_Coffee_Shop_Registration
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
