using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Movies.Common;
using Movies.Common.Validations;
using Movies.Core.Models;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.Areas.Admin.Models
{
    public class GridPersonViewModel : IMap<Person>
    {
        public int Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required(ErrorMessage = "First name is required !")]
        [StringLength(GlobalConstants.MaxPersonNameLength,
            MinimumLength = GlobalConstants.MinPersonNameLength,
            ErrorMessage = "First name should be between 3 and 30 symbols long !")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required !")]
        [StringLength(GlobalConstants.MaxPersonNameLength,
            MinimumLength = GlobalConstants.MinPersonNameLength,
            ErrorMessage = "Last name should be between 3 and 30 symbols long !")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nationality is required !")]
        [StringLength(GlobalConstants.MaxPersonNationalityLength,
            MinimumLength = GlobalConstants.MinPersonNationalityLength,
            ErrorMessage = "Invalid nationality !")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Gender of person is required !")]
        public Gender Gender { get; set; }

        public byte[] Picture { get; set; }

        [Required(ErrorMessage = "Date of birth is required !")]
        [DateValidation]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Role is required !")]
        public MovieRole Role { get; set; }

        public FileContentResult PictureFile { get; set; }
    }
}
