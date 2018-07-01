using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Controllers;
using Movies.Web.ViewModels.UserViewModels;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.UserControllerTests
{
    [TestFixture]
    public class UserProfile_Should
    {
        [Test]
        public void ReturnView_WithUserDetails()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var user = new User()
            {
                UserName = "Username",
                FirstName = "First name",
                LastName = "Last name"
            };

            var userDetails = new UserDetailsViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            userServiceMock.Setup(us => us.GetUser(user.UserName)).Returns(user);
            mapperMock.Setup(m => m.Map<UserDetailsViewModel>(user)).Returns(userDetails);

            // Act
            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            userController
                .WithCallTo(c => c.UserProfile(user.UserName))
                .ShouldRenderDefaultView()
                .WithModel<UserDetailsViewModel>(userDetails);
        }
    }
}
