using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Bytes2you.Validation;
using Movies.Services.Contracts;
using Movies.Web.ViewModels.Movie;

namespace Movies.Web.Controllers
{
    public class HomeController : Controller
    {
        private const int moviesToShow = 10;

        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public HomeController(IMovieService movieService, IMapper mapper)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.movieService = movieService;
        }

        public ActionResult Index()
        {
            var movies = this.movieService
                .GetTopRatedMovies(moviesToShow)
                .Select(m => this.mapper.Map<MovieViewModel>(m));

            return this.View(movies);
        }
    }
}
