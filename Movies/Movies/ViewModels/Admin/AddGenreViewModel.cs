using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Admin
{
    public class AddGenreViewModel : IMap<Genre>
    {
        [Display(Name = "Genre name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(GlobalConstants.MaxGenreNameLength,
            MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "{0} should be between {2} and {1} symbols long!")]
        public string Name { get; set; }
    }
}
