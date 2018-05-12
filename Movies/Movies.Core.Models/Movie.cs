﻿using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class Movie : BaseEntity
    {
        [StringLength(GlobalConstants.MaxMovieLength, MinimumLength = GlobalConstants.MinMovieLength)]
        public string Name { get; set; }
        
        [StringLength(GlobalConstants.MovieYearLength, MinimumLength = GlobalConstants.MovieYearLength)]
        public string Year { get; set; }
        
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime)]
        public int RunningTime { get; set; }
        
        [StringLength(GlobalConstants.MovieDescriptionLength)]
        public string Description { get; set; }

        [Range(GlobalConstants.MinMovieRating, GlobalConstants.MaxMovieRating)]
        public int Rating { get; set; }
    }
}
