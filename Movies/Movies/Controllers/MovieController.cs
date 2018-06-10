﻿using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using Movies.Common;
using Movies.Services.Contracts;
using Movies.Web.ViewModels.Movie;

namespace Movies.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        public MovieController(IMovieService movieService, IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.movieService = movieService;
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