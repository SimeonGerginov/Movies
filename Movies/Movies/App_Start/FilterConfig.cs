using System.Web.Mvc;
using Movies.Infrastructure.Attributes;

namespace Movies.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SaveChangesAttribute());
        }
    }
}
