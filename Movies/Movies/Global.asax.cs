using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Movies.Persistence.Data;
using Movies.Persistence.Data.Migrations;
using Movies.Web.App_Start;

namespace Movies.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsSqlDbContext, Configuration>());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mapper = new AutoMapperConfig();
            mapper.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
