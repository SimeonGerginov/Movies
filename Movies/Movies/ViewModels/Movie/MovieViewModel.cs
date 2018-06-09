using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Movie
{
    public class MovieViewModel : IMap<Core.Models.Movie>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public byte[] Image { get; set; }
    }
}
