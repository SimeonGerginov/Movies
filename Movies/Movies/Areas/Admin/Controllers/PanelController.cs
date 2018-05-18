using System.Web.Mvc;
using Bytes2you.Validation;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.Services.Mappings;
using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.Areas.Admin.Models;

namespace Movies.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly IGenreService genreService;

        public PanelController(IGenreService genreService)
        {
            Guard.WhenArgument(genreService, "Genre Service").IsNull().Throw();

            this.genreService = genreService;
        }

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
        [SaveChanges]
        public ActionResult AddGenre(GenreViewModel genreViewModel)
        {
            if (this.ModelState.IsValid)
            {
                var mappedGenre = MappingService.MappingProvider.Map<Genre>(genreViewModel);
                this.genreService.Add(mappedGenre);
            }

            return this.View();
        }
    }
}