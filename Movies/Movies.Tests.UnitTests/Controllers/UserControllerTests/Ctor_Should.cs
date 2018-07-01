using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.UserControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedUserServiceIsNull()
        {
            // Arrange
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserController(null, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserController(userServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            
            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new UserController(userServiceMock.Object, fileConverterMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfUserController_WhenPassedArgumentsAreValid()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var userController = 
                new UserController(userServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<UserController>(userController);
        }
    }
}
