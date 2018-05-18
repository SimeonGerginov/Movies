using System.Collections.Generic;
using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IGenreService
    {
        void AddGenre(Genre genre);

        bool DeleteGenre(string genreName);

        void UpdateGenre(Genre genreToUpdate);

        IEnumerable<Genre> GetAllGenres();
    }
}