using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.MovieViewModels
{
    public class TopRatedMovieViewModel : IMap<Movie>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public byte[] Image { get; set; }
    }
}
