using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Controllers;
using Movies.Web.ViewModels.UserViewModels;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.UserControllerTests
{
    [TestFixture]
    public class EditUser_Post_Should
    {
        [Test]
        public void OnlyRedirect_WhenModelStateIsValid()
        {
            // Arrange
            var userViewModel = new UserDetailsViewModel()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(userViewModel, null, null);

            var results = new List<ValidationResult>();

            // Act
            var isModelValid = Validator.TryValidateObject(userViewModel, validationContext, results);

            // Assert
            Assert.IsFalse(isModelValid);
        }

        [Test]
        public void NotCallEditUserMethodOfUserService_WhenModelStateIsInvalid()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var userViewModel = new UserDetailsViewModel()
            {
            };

            var userDbModel = new User()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(userViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(userViewModel, validationContext, results);

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            userController.ModelState.AddModelError("username", "No username for user!");

            // Act
            userController.EditUser(userViewModel);

            // Assert
            Assert.IsFalse(isModelValid);
            userServiceMock.Verify(us => us.EditUser(It.IsAny<string>(), userDbModel), Times.Never);
        }

        [Test]
        public void CallEditUserMethodOfUserService_WhenModelStateIsValid()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var userViewModel = new UserDetailsViewModel()
            {
                FirstName = "Ivan",
                LastName = "Stanev",
                Email = "Email",
                Gender = Gender.Male
            };

            var userDbModel = new User()
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Gender = userViewModel.Gender
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(userViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(userViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<User>(userViewModel)).Returns(userDbModel);

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var identityMock = new Mock<IIdentity>();
            var requestMock = new Mock<HttpRequestBase>();
            var filesMock = new Mock<HttpFileCollectionBase>();

            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            contextMock.Setup(c => c.User.Identity).Returns(identityMock.Object);
            identityMock.Setup(i => i.Name).Returns(It.IsAny<string>());
            filesMock.Setup(f => f.Count).Returns(0);
            requestMock.Setup(r => r.Files).Returns(filesMock.Object);
            userController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), userController);

            // Act
            userController.EditUser(userViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            userServiceMock.Verify(us => us.EditUser(It.IsAny<string>(), userDbModel), Times.Once);
        }

        [Test]
        public void CallPostedToByteArrayMethodOfFileConverter_WhenModelStateIsValidAndThereAreFilesInTheRequest()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var userViewModel = new UserDetailsViewModel()
            {
                FirstName = "Ivan",
                LastName = "Stanev",
                Email = "Email",
                Gender = Gender.Male
            };

            var userDbModel = new User()
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Gender = userViewModel.Gender
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(userViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(userViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<User>(userViewModel)).Returns(userDbModel);

            var userController =
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var identityMock = new Mock<IIdentity>();
            var requestMock = new Mock<HttpRequestBase>();
            var filesMock = new Mock<HttpFileCollectionBase>();
            var pictureMock = new Mock<HttpPostedFileBase>();

            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            contextMock.Setup(c => c.User.Identity).Returns(identityMock.Object);
            identityMock.Setup(i => i.Name).Returns(It.IsAny<string>());
            pictureMock.Setup(p => p.ContentLength).Returns(1);
            filesMock.Setup(f => f.Count).Returns(1);
            filesMock.Setup(f => f["ProfilePicture"]).Returns(pictureMock.Object);
            requestMock.Setup(r => r.Files).Returns(filesMock.Object);
            userController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), userController);

            // Act
            userController.EditUser(userViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            fileConverterMock.Verify(fcm => fcm.PostedToByteArray(pictureMock.Object), Times.Once);
        }
    }
}
