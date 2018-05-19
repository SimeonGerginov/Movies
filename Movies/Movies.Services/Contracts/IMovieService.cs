using System.Collections.Generic;
using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IMovieService
    {
        void AddMovie(Movie movie);

        bool DeleteMovie(string movieName);

        void UpdateMovie(Movie movieToUpdate);

        IEnumerable<Movie> GetAllMovies();
    }
}