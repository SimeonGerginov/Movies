using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface IGenreService
    {
        void Add(Genre genre);
    }
}