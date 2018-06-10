using AutoMapper;

using Movies.Core.Models.Enums;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Person
{
    public class MovieParticipantViewModel : ICustomMappings
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Nationality { get; set; }

        public Role Role { get; set; }

        public byte[] Picture { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Core.Models.Person, MovieParticipantViewModel>()
                .ForMember(p => p.FullName, opt => opt.MapFrom(p => p.FirstName + " " + p.LastName));
        }
    }
}
