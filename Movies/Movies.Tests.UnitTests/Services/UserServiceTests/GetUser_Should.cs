using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.UserServiceTests
{
    [TestFixture]
    public class GetUser_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserIsNotFound()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object);
            var username = "Username";

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => userService.GetUser(username));
        }

        [Test]
        public void ReturnTheUser_WithThePassedUsername()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            var user = new User()
            {
                UserName = "Username",
                FirstName = "First name",
                LastName = "Last name",
                Email = "Email"
            };

            ICollection<User> filteredUsers = new List<User>();
            filteredUsers.Add(user);

            userRepositoryMock.Setup(ur => ur.GetAllFiltered(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(filteredUsers);

            var userService = new UserService(userRepositoryMock.Object);

            // Act
            var expectedUser = userService.GetUser(user.UserName);

            // Assert
            Assert.AreEqual(expectedUser, user);
        }
    }
}
