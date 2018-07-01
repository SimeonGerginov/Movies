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
    public class AddPerson_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedPersonIsNull()
        {
            // Arrange
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => personService.AddPerson(null));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenPassedPersonExists()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            ICollection<Person> filteredPeople = new List<Person>();
            filteredPeople.Add(personMock.Object);

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => personService.AddPerson(personMock.Object));
        }

        [Test]
        public void CallAddMethodOfRepositoryOnce_WhenPassedPersonDoesNotExists()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            IEnumerable<Person> filteredPeople = new List<Person>();

            personRepositoryMock.Setup(pr => pr.GetAllFiltered(It.IsAny<Expression<Func<Person, bool>>>()))
                .Returns(filteredPeople);

            // Act
            personService.AddPerson(personMock.Object);

            // Assert
            personRepositoryMock.Verify(pr => pr.Add(personMock.Object), Times.Once);
        }
    }
}
