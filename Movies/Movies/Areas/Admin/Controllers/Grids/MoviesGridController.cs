using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.ViewModels.Grid;

using WebGrease.Css.Extensions;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class MoviesGridController : AdminController
    {
        private readonly IMovieService movieService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        private IEnumerable<GridMovieViewModel> movies;
        private IDictionary<int, FileContentResult> images;

        public MoviesGridController(IMovieService movieService, IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();

            this.movieService = movieService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;

            this.images = new Dictionary<int, FileContentResult>();
            this.GetMovies();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadMovies([DataSourceRequest] DataSourceRequest request)
        {
            var movies = this.movies.ToDataSourceResult(request);

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

        public FileContentResult GetImage(string id)
        {
            return this.images[int.Parse(id)];
        }

        private void GetMovies()
        {
            this.movies = this.movieService
                .GetAllMovies()
                .Select(m => this.mapper.Map<GridMovieViewModel>(m));

            this.movies.ForEach(m => m.ImageFile = this.SetMovieImage(m));
        }

        private FileContentResult SetMovieImage(GridMovieViewModel movie)
        {
            var image = movie.Image;

            if (image == null)
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                var file = this.File(defaultImage, "image/png");

                this.images.Add(movie.Id, file);

                return file;
            }
            else
            {
                var file = this.File(image, "image/jpeg");
                this.images.Add(movie.Id, file);

                return file;
            }
        }
    }
}
