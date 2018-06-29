using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Movies.Core.Models.Enums;

namespace Movies.Web.ViewModels.Admin
{
    public class PersonInMovieViewModel
    {
        [Display(Name = "Person")]
        public int PersonId { get; set; }

        [Display(Name = "Movie")]
        public int MovieId { get; set; }

        [Display(Name = "Role in movie")]
        public Role Role { get; set; }

        public IEnumerable<SelectListItem> People { get; set; }

        public IEnumerable<SelectListItem> Movies { get; set; }
    }
}
