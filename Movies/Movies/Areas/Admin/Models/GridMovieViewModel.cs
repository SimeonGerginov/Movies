using System;
using AutoMapper;

using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class GridMovieViewModel : IMap<Movie>, ICustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string Year { get; set; }
        
        public int RunningTime { get; set; }
        
        public string Description { get; set; }
        
        public int Rating { get; set; }

        public string GenreName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Movie, GridMovieViewModel>()
                .ForMember(m => m.GenreName, opt => opt.MapFrom(m => m.Genre.Name));
        }
    }
}
