﻿using System;
using System.ComponentModel.DataAnnotations;

using AutoMapper;

using Movies.Common;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.ViewModels.GridViewModels
{
    public class GridMovieViewModel : IMap<Movie>, ICustomMappings
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The name is required !")]
        [StringLength(GlobalConstants.MaxMovieLength,
            MinimumLength = GlobalConstants.MinMovieLength,
            ErrorMessage = "Movie name should be between 3 and 40 symbols long !")]
        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required(ErrorMessage = "The year is required !")]
        [StringLength(GlobalConstants.MovieYearLength,
            MinimumLength = GlobalConstants.MovieYearLength,
            ErrorMessage = "Movie year should be 4 symbols long !")]
        public string Year { get; set; }

        [Required(ErrorMessage = "Running time is required !")]
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime,
            ErrorMessage = "Movie running time should be between 10 and 600 minutes long !")]
        public int RunningTime { get; set; }

        [Required(ErrorMessage = "Rating is required !")]
        [Range(GlobalConstants.MinMovieRating, GlobalConstants.MaxMovieRating,
            ErrorMessage = "Movie rating should be between 1 and 10 !")]
        public int Rating { get; set; }

        public string GenreName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Movie, GridMovieViewModel>()
                .ForMember(m => m.GenreName, opt => opt.MapFrom(m => m.Genre.Name));
        }
    }
}
