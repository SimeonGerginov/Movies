using System;
using System.ComponentModel.DataAnnotations;

using Movies.Common;
using Movies.Common.Validations;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Infrastructure.Contracts;

namespace Movies.Web.ViewModels.Admin
{
    public class PersonViewModel : IMap<Person>
    {
        [Display(Name = "First name of person")]
        [Required(ErrorMessage = "First name is required !")]
        [StringLength(GlobalConstants.MaxPersonNameLength, 
            MinimumLength = GlobalConstants.MinPersonNameLength,
            ErrorMessage = "First name should be between 3 and 30 symbols long !")]
        public string FirstName { get; set; }

        [Display(Name = "Last name of person")]
        [Required(ErrorMessage = "Last name is required !")]
        [StringLength(GlobalConstants.MaxPersonNameLength, 
            MinimumLength = GlobalConstants.MinPersonNameLength,
            ErrorMessage = "Last name should be between 3 and 30 symbols long !")]
        public string LastName { get; set; }

        [Display(Name = "Nationality of person")]
        [Required(ErrorMessage = "Nationality is required !")]
        [StringLength(GlobalConstants.MaxPersonNationalityLength, 
            MinimumLength = GlobalConstants.MinPersonNationalityLength, 
            ErrorMessage = "Invalid nationality !")]
        public string Nationality { get; set; }

        [Display(Name = "Gender of person")]
        [Required(ErrorMessage = "Gender of person is required !")]
        public Gender Gender { get; set; }

        [Display(Name = "Picture of person")]
        public byte[] Picture { get; set; }

        [Display(Name = "Date of birth of person")]
        [Required(ErrorMessage = "Date of birth is required !")]
        [DateValidation]
        public DateTime DateOfBirth { get; set; }
    }
}
