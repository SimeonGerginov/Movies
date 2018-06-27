using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Admin
{
    public class MovieViewModel : IMap<Movie>
    {
        public IEnumerable<SelectListItem> GenresSelectList { get; set; }

        [Display(Name = "Name of movie")]
        [Required(ErrorMessage = "The name is required !")]
        [StringLength(GlobalConstants.MaxMovieLength,
            MinimumLength = GlobalConstants.MinMovieLength,
            ErrorMessage = "Movie name should be between 3 and 40 symbols long !")]
        public string Name { get; set; }

        [Display(Name = "Year of movie")]
        [Required(ErrorMessage = "The year is required !")]
        [StringLength(GlobalConstants.MovieYearLength,
            MinimumLength = GlobalConstants.MovieYearLength,
            ErrorMessage = "Movie year should be 4 symbols long !")]
        public string Year { get; set; }

        [Display(Name = "Running time of movie")]
        [Required(ErrorMessage = "Running time is required !")]
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime,
            ErrorMessage = "Movie running time should be between 10 and 600 minutes long !")]
        public int RunningTime { get; set; }

        [Display(Name = "Description of movie")]
        [Required(ErrorMessage = "Description is required !")]
        [StringLength(GlobalConstants.MovieDescriptionLength, 
            ErrorMessage = "Movie description should be no more than 200 symbols long !")]
        public string Description { get; set; }

        [Display(Name = "Genre of movie")]
        [Required(ErrorMessage = "Genre is required !")]
        [StringLength(GlobalConstants.MaxGenreNameLength, MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "Genre name should be between 3 and 20 symbols long !")]
        public string GenreName { get; set; }

        [Display(Name = "Movie image")]
        public byte[] Image { get; set; }
    }
}
