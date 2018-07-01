using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.PersonServiceTests
{
    [TestFixture]
    public class DeletePerson_Should
    {
        [Test]
        public void ReturnFalse_WhenPassedPersonDoesNotExist()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            IEnumerable<Person> filteredPeople = new List<Person>();

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act
            var result = personService.DeletePerson(personMock.Object.Id);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CallDeleteMethodOfRepositoryOnce_WhenPassedPersonExists()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            ICollection<Person> filteredPeople = new List<Person>();
            filteredPeople.Add(personMock.Object);

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act
            personService.DeletePerson(personMock.Object.Id);

            // Assert
            personRepositoryMock.Verify(pr => pr.Delete(personMock.Object), Times.Once);
        }

        [Test]
        public void ReturnTrue_WhenPassedPersonExists()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            ICollection<Person> filteredPeople = new List<Person>();
            filteredPeople.Add(personMock.Object);

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act
            var result = personService.DeletePerson(personMock.Object.Id);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
