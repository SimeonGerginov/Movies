using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class MovieViewModel : IMap<Movie>
    {
        public IEnumerable<SelectListItem> GenresSelectList { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxMovieLength, MinimumLength = GlobalConstants.MinMovieLength,
            ErrorMessage = "Movie name should be between 3 and 40 symbols long !")]
        public string Name { get; set; }

        [Required]
        [StringLength(GlobalConstants.MovieYearLength, MinimumLength = GlobalConstants.MovieYearLength,
            ErrorMessage = "Movie year should be 4 symbols long !")]
        public string Year { get; set; }

        [Required]
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime,
            ErrorMessage = "Movie running time should be between 10 and 600 minutes long !")]
        public int RunningTime { get; set; }

        [Required]
        [StringLength(GlobalConstants.MovieDescriptionLength, 
            ErrorMessage = "Movie description should be not more than 200 symbols long !")]
        public string Description { get; set; }

        [Required]
        [Range(GlobalConstants.MinMovieRating, GlobalConstants.MaxMovieRating,
            ErrorMessage = "Movie rating should be between 1 and 10 !")]
        public int Rating { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxGenreNameLength, MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "Genre name should be between 3 and 20 symbols long !")]
        public string GenreName { get; set; }
    }
}
