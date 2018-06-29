using System.Collections.Generic;
using AutoMapper;

using Movies.Infrastructure.Contracts;
using Movies.Web.ViewModels.PersonViewModels;

namespace Movies.Web.ViewModels.MovieViewModels
{
    public class MovieDetailsViewModel : ICustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Year { get; set; }
        
        public int RunningTime { get; set; }
        
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string GenreName { get; set; }

        public IEnumerable<MovieParticipantViewModel> Participants { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Core.Models.Movie, MovieDetailsViewModel>()
                .ForMember(m => m.GenreName, opt => opt.MapFrom(m => m.Genre.Name))
                .ForMember(m => m.Participants, opt => opt.MapFrom(m => m.People));
        }
    }
}
