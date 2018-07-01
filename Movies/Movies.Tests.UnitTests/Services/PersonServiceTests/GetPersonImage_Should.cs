using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.PersonServiceTests
{
    [TestFixture]
    public class GetPersonImage_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPersonWithThePassedIdDoesNotExist()
        {
            // Arrange
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);
            var personId = -1;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => personService.GetPersonImage(personId));
        }

        [Test]
        public void ReturnPersonImage_WhenPersonWithThePassedIdExists()
        {
            // Arrange
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var picture = new byte[128];

            var person = new Person()
            {
                Id = 1,
                FirstName = "Person first name",
                LastName = "Person last name",
                Gender = Gender.Male,
                DateOfBirth = DateTime.UtcNow,
                Picture = picture
            };

            personRepositoryMock.Setup(pr => pr.GetById(person.Id)).Returns(person);

            var personService = new PersonService(personRepositoryMock.Object);

            // Act
            var expectedPicture = personService.GetPersonImage(person.Id);

            // Assert
            Assert.AreEqual(expectedPicture, person.Picture);
        }
    }
}
