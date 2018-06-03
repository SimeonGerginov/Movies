using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.ViewModels.GridViewModels;
using Movies.Web.Areas.Admin.Controllers.Abstraction;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class MoviesGridController : AdminController
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MoviesGridController(IMovieService movieService, IMapper mapper)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.movieService = movieService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadMovies([DataSourceRequest] DataSourceRequest request)
        {
            var movies = this.movieService
                .GetAllMovies()
                .Select(m => this.mapper.Map<GridMovieViewModel>(m))
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
                var movie = this.mapper.Map<Movie>(movieModel);
                this.movieService.UpdateMovie(movie);
            }

            return this.Json(new[] { movieModel });
        }
    }
}
