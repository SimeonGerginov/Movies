using System.Collections.Generic;
using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IMovieService
    {
        void AddMovie(Movie movie, string genreName);

        void AddPersonToMovie(int movieId, int personId);

        bool DeleteMovie(int movieId);

        void UpdateMovie(Movie movieToUpdate);

        IEnumerable<Movie> GetAllMovies();
    }
}