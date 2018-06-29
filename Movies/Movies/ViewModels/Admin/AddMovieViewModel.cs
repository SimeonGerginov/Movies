using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Admin
{
    public class AddMovieViewModel : IMap<Movie>
    {
        public IEnumerable<SelectListItem> GenresSelectList { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(GlobalConstants.MaxMovieLength,
            MinimumLength = GlobalConstants.MinMovieLength,
            ErrorMessage = "{0} should be between {2} and {1} symbols long!")]
        public string Name { get; set; }

        [Display(Name = "Movie year")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(GlobalConstants.MovieYearLength,
            MinimumLength = GlobalConstants.MovieYearLength,
            ErrorMessage = "{0} should be {1} symbols long!")]
        public string Year { get; set; }

        [Display(Name = "Movie running time")]
        [Required(ErrorMessage = "{0} is required!")]
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime,
            ErrorMessage = "{0} should be between {1} and {2} minutes long!")]
        public int RunningTime { get; set; }

        [Display(Name = "Movie description")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(GlobalConstants.MovieDescriptionLength, 
            ErrorMessage = "{0} should not be more than {1} symbols long!")]
        public string Description { get; set; }

        [Display(Name = "Movie genre")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(GlobalConstants.MaxGenreNameLength, MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "{0} should be between {2} and {1} symbols long!")]
        public string GenreName { get; set; }

        [Display(Name = "Movie image")]
        public byte[] Image { get; set; }
    }
}
