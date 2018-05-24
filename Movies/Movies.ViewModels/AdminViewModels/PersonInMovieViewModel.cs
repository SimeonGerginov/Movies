using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Movies.ViewModels.AdminViewModels
{
    public class PersonInMovieViewModel
    {
        [Display(Name = "Person")]
        public int PersonId { get; set; }

        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        public IEnumerable<SelectListItem> People { get; set; }

        public IEnumerable<SelectListItem> Movies { get; set; }
    }
}
