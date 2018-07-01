using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Controllers;
using Movies.Web.ViewModels.UserViewModels;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.UserControllerTests
{
    [TestFixture]
    public class EditUser_Should
    {
        [Test]
        public void RenderEditUserViewWithUserViewModel()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var httpContextMock = new Mock<HttpContextBase>();
            var controllerContextMock = new Mock<ControllerContext>();
            var identity = new GenericIdentity("User");
            var principal = new GenericPrincipal(identity, null);

            var user = new User()
            {
                FirstName = "First name",
                LastName = "Last name",
                Email = "Email",
                Gender = Gender.Male
            };

            var userViewModel = new UserDetailsViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender
            };

            userServiceMock.Setup(us => us.GetUser(It.IsAny<string>())).Returns(user);
            mapperMock.Setup(m => m.Map<UserDetailsViewModel>(user)).Returns(userViewModel);
            httpContextMock.Setup(c => c.User).Returns(principal);
            controllerContextMock.Setup(c => c.HttpContext).Returns(httpContextMock.Object);

            // Act
            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            userController.ControllerContext = controllerContextMock.Object;

            // Assert
            userController
                .WithCallTo(c => c.EditUser())
                .ShouldRenderDefaultView()
                .WithModel<UserDetailsViewModel>(userViewModel);
        }
    }
}
