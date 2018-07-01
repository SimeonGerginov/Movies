using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.UserControllerTests
{
    [TestFixture]
    public class UserPhoto_Should
    {
        [Test]
        public void GetDefaultPicture_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var identityMock = new Mock<IIdentity>();

            contextMock.Setup(c => c.User.Identity).Returns(identityMock.Object);
            identityMock.Setup(i => i.IsAuthenticated).Returns(false);
            userController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), userController);

            // Act
            userController.UserPhoto(It.IsAny<string>());

            // Assert
            fileConverterMock.Verify(fcm => fcm.GetDefaultPicture(), Times.Once);
        }

        [Test]
        public void GetDefaultPicture_WhenUserPictureIsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var user = new User()
            {
                UserName = "Username",
                ProfilePicture = null
            };

            userServiceMock.Setup(us => us.GetUser(user.UserName)).Returns(user);

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var identityMock = new Mock<IIdentity>();

            contextMock.Setup(c => c.User.Identity).Returns(identityMock.Object);
            identityMock.Setup(i => i.IsAuthenticated).Returns(true);
            userController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), userController);

            // Act
            userController.UserPhoto(user.UserName);

            // Assert
            fileConverterMock.Verify(fcm => fcm.GetDefaultPicture(), Times.Once);
        }

        [Test]
        public void GetPicture_WhenUserPictureIsNotNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var profilePicture = new byte[128];

            var user = new User()
            {
                UserName = "Username",
                ProfilePicture = profilePicture
            };

            userServiceMock.Setup(us => us.GetUser(user.UserName)).Returns(user);

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var identityMock = new Mock<IIdentity>();

            contextMock.Setup(c => c.User.Identity).Returns(identityMock.Object);
            identityMock.Setup(i => i.IsAuthenticated).Returns(true);
            userController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), userController);

            // Act
            userController.UserPhoto(user.UserName);

            // Assert
            fileConverterMock.Verify(fcm => fcm.GetDefaultPicture(), Times.Never);
        }
    }
}
