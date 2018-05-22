using System;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Common.Validations;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class PersonViewModel : IMap<Person>
    {
        [Required]
        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxPersonNameLength, MinimumLength = GlobalConstants.MinPersonNameLength)]
        public string LastName { get; set; }

        [Required]
        [StringLength(GlobalConstants.MaxPersonNationalityLength, MinimumLength = GlobalConstants.MinPersonNationalityLength)]
        public string Nationality { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public byte[] Picture { get; set; }

        [Required]
        [DateValidation]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public MovieRole Role { get; set; }
    }
}
