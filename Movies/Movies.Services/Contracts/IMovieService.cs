using System.Collections.Generic;

using Movies.Core.Models;
using Movies.Core.Models.Enums;

namespace Movies.Services.Contracts
{
    public interface IMovieService
    {
        void AddMovie(Movie movie, string genreName);

        void AddPersonToMovie(int movieId, int personId, Role role);

        bool DeleteMovie(int movieId);

        void UpdateMovie(Movie movieToUpdate);

        byte[] GetMovieImage(int movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetTopRatedMovies(int moviesToTake);
    }
}
