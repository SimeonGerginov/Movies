﻿using System;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Grid
{
    public class GridGenreViewModel : IMap<Genre>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required !")]
        [StringLength(GlobalConstants.MaxGenreNameLength,
            MinimumLength = GlobalConstants.MinGenreNameLength,
            ErrorMessage = "Genre name should be between 3 and 20 symbols long !")]
        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
