using System.Web.Mvc;
using Bytes2you.Validation;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Infrastructure.Extensions;
using Movies.Services.Contracts;
using Movies.Services.Mappings;
using Movies.ViewModels.GridViewModels;
using Movies.Web.Areas.Admin.Controllers.Abstraction;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class MoviesGridController : AdminController
    {
        private readonly IMovieService movieService;

        public MoviesGridController(IMovieService movieService)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();

            this.movieService = movieService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadMovies([DataSourceRequest] DataSourceRequest request)
        {
            var movies = this.movieService
                .GetAllMovies()
                .Map<Movie, GridMovieViewModel>()
                .ToDataSourceResult(request);

            return this.Json(movies);
        }

        [SaveChanges]
        public ActionResult DeleteMovie(GridMovieViewModel movieModel)
        {
            if (movieModel != null)
            {
                this.movieService.DeleteMovie(movieModel.Id);
            }

            return this.Json(new[] { movieModel });
        }

        [SaveChanges]
        public ActionResult EditMovie(GridMovieViewModel movieModel)
        {
            if (movieModel != null)
            {
                var movie = MappingService.MappingProvider.Map<Movie>(movieModel);
                this.movieService.UpdateMovie(movie);
            }

            return this.Json(new[] { movieModel });
        }
    }
}
