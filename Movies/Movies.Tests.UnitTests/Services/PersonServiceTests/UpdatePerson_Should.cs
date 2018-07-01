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
    public class UpdatePerson_Should
    {
        [Test]
        public void NotCallUpdateMethodOfRepository_WhenPassedPersonDoesNotExist()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            IEnumerable<Person> filteredPeople = new List<Person>();

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act
            personService.UpdatePerson(personMock.Object);

            // Assert
            personRepositoryMock.Verify(pr => pr.Update(personMock.Object), Times.Never);
        }

        [Test]
        public void CallUpdateMethodOfRepositoryOnce_WhenPassedPersonExists()
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
            personService.UpdatePerson(personMock.Object);

            // Assert
            personRepositoryMock.Verify(pr => pr.Update(personMock.Object), Times.Once);
        }
    }
}
