using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.UserServiceTests
{
    [TestFixture]
    public class EditUser_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenThePassedUserIdIsNull()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => userService.EditUser(null, It.IsAny<User>()));
        }

        [Test]
        public void NotCallUpdateMethodOfRepository_WhenUserWithThePassedIdIsNotFound()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            var userService = new UserService(userRepositoryMock.Object);
            var userId = "userId";

            // Act
            userService.EditUser(userId, It.IsAny<User>());

            // Assert
            userRepositoryMock.Verify(ur => ur.Update(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public void CallUpdateMethodOfRepositoryOnce_WhenUserWithThePassedIdIsFound()
        {
            // Arrange
            var userRepositoryMock = new Mock<IRepository<User>>();
            
            var profilePicture = new byte[128];
            var user = new User()
            {
                Id = "Id",
                FirstName = "User first name",
                LastName = "User last name",
                Email = "Email",
                Gender = Gender.Male,
                ProfilePicture = profilePicture
            };

            ICollection<User> filteredUsers = new List<User>();
            filteredUsers.Add(user);

            userRepositoryMock.Setup(ur => ur.GetAllFiltered(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(filteredUsers);

            var userService = new UserService(userRepositoryMock.Object);

            // Act
            userService.EditUser(user.Id, user);

            // Assert
            userRepositoryMock.Verify(ur => ur.Update(It.IsAny<User>()), Times.Once);
        }
    }
}
