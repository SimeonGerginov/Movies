using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using Bytes2you.Validation;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.Services.Mappings;
using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.Areas.Admin.Models;

namespace Movies.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly IGenreService genreService;
        private readonly IMovieService movieService;

        public PanelController(IGenreService genreService, IMovieService movieService)
        {
            Guard.WhenArgument(genreService, "Genre Service").IsNull().Throw();
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();

            this.genreService = genreService;
            this.movieService = movieService;
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
                this.genreService.AddGenre(mappedGenre);
            }

            return this.RedirectToAction<GenresGridController>(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddMovie()
        {
            var genresSelectList = this.genreService
                .GetAllGenres()
                .Select(g => new SelectListItem() { Text = g.Name, Value = g.Name });

            var movieViewModel = new MovieViewModel()
            {
                GenresSelectList = genresSelectList
            };

            return this.PartialView(PartialViews.AddMovie, movieViewModel);
        }
    }
}
