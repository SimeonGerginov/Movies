﻿using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class GenreViewModel : IMap<Genre>
    {
        [Display(Name = "Name of genre")]
        [Required(ErrorMessage = "The name is required !")]
        [StringLength(GlobalConstants.MaxGenreNameLength,
            MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "Genre name should be between 3 and 20 symbols long !")]
        public string Name { get; set; }
    }
}