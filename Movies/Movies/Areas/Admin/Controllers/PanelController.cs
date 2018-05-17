using System.Web.Mvc;
using Movies.Areas.Admin.Controllers.Abstraction;

namespace Movies.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}