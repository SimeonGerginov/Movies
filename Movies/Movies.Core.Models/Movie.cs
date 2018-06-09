using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class Movie : BaseEntity
    {
        private ICollection<Person> people;
        private ICollection<MovieRole> roles;
        private ICollection<MovieRating> ratings;
        private ICollection<Comment> comments;

        public Movie()
        {
            this.people = new HashSet<Person>();
            this.roles = new HashSet<MovieRole>();
            this.ratings = new HashSet<MovieRating>();
            this.comments = new HashSet<Comment>();
        }

        [StringLength(GlobalConstants.MaxMovieLength, MinimumLength = GlobalConstants.MinMovieLength)]
        public string Name { get; set; }
        
        [StringLength(GlobalConstants.MovieYearLength, MinimumLength = GlobalConstants.MovieYearLength)]
        public string Year { get; set; }
        
        [Range(GlobalConstants.MinMovieRunningTime, GlobalConstants.MaxMovieRunningTime)]
        public int RunningTime { get; set; }
        
        [StringLength(GlobalConstants.MovieDescriptionLength)]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<Person> People
        {
            get { return this.people; }
            set { this.people = value; }
        }

        public virtual ICollection<MovieRole> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }

        public virtual ICollection<MovieRating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
