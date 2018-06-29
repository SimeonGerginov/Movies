using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Movies.Common;
using Movies.Core.Contracts;
using Movies.Core.Models.Enums;

namespace Movies.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser, IAuditable, IDeletable
    {
        private ICollection<MovieRating> ratings;
        private ICollection<Comment> comments;

        public User()
        {
            this.ratings = new HashSet<MovieRating>();
            this.comments = new HashSet<Comment>();
        }

        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string FirstName { get; set; }

        [StringLength(GlobalConstants.MaxUserNameLength, MinimumLength = GlobalConstants.MinUserNameLength)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<MovieRating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
