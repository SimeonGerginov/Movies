using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Common.Validations;
using Movies.Core.Entities;

namespace Movies.Core.Models
{
    public class Person : BaseEntity
    {
        private ICollection<Movie> movies;

        public Person()
        {
            this.movies = new HashSet<Movie>();
        }

        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string FirstName { get; set; }

        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string LastName { get; set; }

        [StringLength(GlobalConstants.MaxPersonNationalityLength, MinimumLength = GlobalConstants.MinPersonNationalityLength)]
        public string Nationality { get; set; }

        [Range(GlobalConstants.MinPersonAge, GlobalConstants.MaxPersonAge)]
        public int Age { get; set; }

        public Gender Gender { get; set; }

        public byte[] Picture { get; set; }

        [DateValidation]
        public DateTime DateOfBirth { get; set; }

        public MovieRole Role { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }
    }
}
