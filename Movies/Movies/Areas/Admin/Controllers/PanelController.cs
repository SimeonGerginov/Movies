using System.Web.Mvc;

using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.Areas.Admin.Models;
using Movies.Common;
using Movies.Infrastructure.Attributes;

namespace Movies.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddGenre()
        {
            return this.PartialView(PartialViews.AddGenre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(GenreViewModel genreViewModel)
        {
            return this.View();
        }
    }
}