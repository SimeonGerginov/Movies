using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;
using Movies.Services.Contracts;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.UserServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Throw_WhenPassedRepositoryIsNull()
        {
            // Arrange && Act && Assert
            Assert.Throws<ArgumentNullException>(() => new UserService(null));
        }

        [Test]
        public void ReturnAnInstanceOfIUserService_WhenValidRepositoryIsPassed()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();

            // Act
            var userService = new UserService(userRepositoryMock.Object);

            // Assert
            Assert.IsInstanceOf<IUserService>(userService);
        }
    }
}
