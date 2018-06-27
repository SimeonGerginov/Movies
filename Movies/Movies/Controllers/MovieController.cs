using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using Movies.Common;
using Movies.Services.Contracts;
using Movies.Web.ViewModels.MovieViewModels;

namespace Movies.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IMovieRoleService movieRoleService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        public MovieController(IMovieService movieService, IMovieRoleService movieRoleService,
            IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(movieRoleService, "Movie Role Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.movieService = movieService;
            this.movieRoleService = movieRoleService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;
        }

        [ChildActionOnly]
        public ActionResult GetTopRatedMovies()
        {
            var topRatedMovies = this.movieService
                .GetTopRatedMovies(GlobalConstants.TopRatedMoviesCount)
                .Select(m => this.mapper.Map<TopRatedMovieViewModel>(m));

            return this.PartialView(PartialViews.TopRatedMovies, topRatedMovies);
        }

        [ChildActionOnly]
        public ActionResult MovieDetails(int movieId)
        {
            var movie = this.movieService.GetMovie(movieId);
            var movieVm = this.mapper.Map<MovieDetailsViewModel>(movie);

            foreach (var participant in movieVm.Participants)
            {
                participant.Role = this.movieRoleService.GetRoleInMovie(movieVm.Id, participant.Id);
            }

            return this.PartialView(PartialViews.MovieDetails, movieVm);
        }

        public ActionResult MovieImage(int movieId)
        {
            var image = this.movieService.GetMovieImage(movieId);

            if (image == null)
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                var file = this.File(defaultImage, "image/png");

                return file;
            }
            else
            {
                var file = this.File(image, "image/jpeg");

                return file;
            }
        }
    }
}
