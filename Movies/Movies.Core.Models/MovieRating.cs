using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class MovieRating : BaseEntity
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Range(GlobalConstants.MinMovieRating, GlobalConstants.MaxMovieRating)]
        public int Rating { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
