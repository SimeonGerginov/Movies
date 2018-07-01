using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;
using Movies.Services.Contracts;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.PersonServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Throw_WhenPassedRepositoryIsNull()
        {
            // Arrange && Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PersonService(null));
        }

        [Test]
        public void ReturnAnInstanceOfIPersonService_WhenValidRepositoryIsPassed()
        {
            // Arrange
            var personRepositoryMock = new Mock<IRepository<Person>>();

            // Act
            var personService = new PersonService(personRepositoryMock.Object);

            // Assert
            Assert.IsInstanceOf<IPersonService>(personService);
        }
    }
}
