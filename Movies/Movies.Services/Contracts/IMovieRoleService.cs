using Movies.Core.Models.Enums;

namespace Movies.Services.Contracts
{
    public interface IMovieRoleService
    {
        Role GetRoleInMovie(int movieId, int participantId);
    }
}
