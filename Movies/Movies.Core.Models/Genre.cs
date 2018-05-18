using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class Genre : BaseEntity
    {
        private ICollection<Movie> movies;

        public Genre()
        {
            this.movies = new HashSet<Movie>();
        }

        [StringLength(GlobalConstants.MaxGenreNameLength, MinimumLength = GlobalConstants.MinGenreNameLength)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }
    }
}
