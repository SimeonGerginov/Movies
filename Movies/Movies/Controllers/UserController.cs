using System.Web.Mvc;
using AutoMapper;
using Bytes2you.Validation;
using Microsoft.AspNet.Identity;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.Web.ViewModels.UserViewModels;

namespace Movies.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(userService, "User Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.userService = userService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            var username = this.HttpContext.User.Identity.Name;
            var user = this.userService.GetUser(username);
            var userViewModel = this.mapper.Map<UserDetailsViewModel>(user);

            return this.View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult EditUser([Bind(Exclude = "ProfilePicture")]UserDetailsViewModel userVm)
        {
            if (this.ModelState.IsValid)
            {
                if (this.Request.Files.Count > 0)
                {
                    var profilePicture = this.Request.Files["ProfilePicture"];

                    if (profilePicture.ContentLength > 0)
                    {
                        var imageData = this.fileConverter.PostedToByteArray(profilePicture);
                        userVm.ProfilePicture = imageData;
                    }
                }

                var userId = this.User.Identity.GetUserId();
                var user = this.mapper.Map<User>(userVm);

                this.userService.EditUser(userId, user);

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(userVm);
        }

        public ActionResult UserProfile(string username)
        {
            var user = this.userService.GetUser(username);
            var userDetails = this.mapper.Map<UserDetailsViewModel>(user);

            return this.View(userDetails);
        }

        public FileContentResult UserPhoto(string username)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = this.userService.GetUser(username);
                var userProfilePicture = user.ProfilePicture;

                if (userProfilePicture == null)
                {
                    var defaultImage = this.fileConverter.GetDefaultPicture();
                    var file = this.File(defaultImage, "image/png");

                    return file;
                }
                else
                {
                    var file = this.File(userProfilePicture, "image/jpeg");
                    
                    return file;
                }
            }
            else
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                var file = this.File(defaultImage, "image/png");

                return file;
            }
        }
    }
}
