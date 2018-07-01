using System.Collections.Generic;
using System.Linq;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.PersonServiceTests
{
    [TestFixture]
    public class GetAllPeople_Should
    {
        [Test]
        public void ReturnAllPeopleFromRepository()
        {
            // Arrange
            var personMock = new Mock<Person>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var personService = new PersonService(personRepositoryMock.Object);

            ICollection<Person> peopleFromRepository = new List<Person>();
            peopleFromRepository.Add(personMock.Object);

            personRepositoryMock.Setup(pr => pr.GetAll())
                .Returns(peopleFromRepository);

            // Act
            var people = personService.GetAllPeople();

            // Assert
            Assert.AreEqual(people.Count(), peopleFromRepository.Count);
            CollectionAssert.Contains(people, personMock.Object);
        }
    }
}
