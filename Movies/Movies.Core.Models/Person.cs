using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Movies.Common;
using Movies.Common.Validations;
using Movies.Core.Entities;
using Movies.Core.Models.Enums;

namespace Movies.Core.Models
{
    public class Person : BaseEntity
    {
        private ICollection<Movie> movies;
        private ICollection<MovieRole> roles;

        public Person()
        {
            this.movies = new HashSet<Movie>();
            this.roles = new HashSet<MovieRole>();
        }

        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string FirstName { get; set; }

        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string LastName { get; set; }

        [StringLength(GlobalConstants.MaxPersonNationalityLength, MinimumLength = GlobalConstants.MinPersonNationalityLength)]
        public string Nationality { get; set; }

        [Range(GlobalConstants.MinPersonAge, GlobalConstants.MaxPersonAge)]
        public int Age { get; private set; }

        public Gender Gender { get; set; }

        public byte[] Picture { get; set; }

        [DateValidation]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get { return this.movies; }
            set { this.movies = value; }
        }

        public virtual ICollection<MovieRole> Roles
        {
            get { return this.roles; }
            set { this.roles = value; }
        }

        public void SetPersonAge()
        {
            this.Age = DateTime.UtcNow.Year - this.DateOfBirth.Year;
        }
    }
}
