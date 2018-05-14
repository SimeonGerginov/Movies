using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Contracts;

namespace Movies.Core.Models
{
    public class Genre : IBaseEntity
    {
        private ICollection<Movie> movies;

        public Genre()
        {
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [StringLength(GlobalConstants.MaxGenreNameLength, MinimumLength = GlobalConstants.MinGenreNameLength)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }
    }
}
