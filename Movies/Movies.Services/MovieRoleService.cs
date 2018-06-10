using System.Linq;
using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class MovieRoleService : IMovieRoleService
    {
        private readonly IRepository<MovieRole> movieRoleRepository;

        public MovieRoleService(IRepository<MovieRole> movieRoleRepository)
        {
            Guard.WhenArgument(movieRoleRepository, "Movie Role Repository").IsNull().Throw();
            
            this.movieRoleRepository = movieRoleRepository;
        }

        public Role GetRoleInMovie(int movieId, int participantId)
        {
            var movieRole = this.movieRoleRepository
                .GetAllFiltered(mr => mr.MovieId == movieId && mr.PersonId == participantId)
                .FirstOrDefault();
            
            Guard.WhenArgument(movieRole, "Movie Role").IsNull().Throw();

            return movieRole.Role;
        }
    }
}
