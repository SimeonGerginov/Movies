using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;
using Movies.Services.Contracts;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.GenreServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Throw_WhenPassedRepositoryIsNull()
        {
            // Arrange && Act && Assert
            Assert.Throws<ArgumentNullException>(() => new GenreService(null));
        }

        [Test]
        public void ReturnAnInstanceOfIGenreService_WhenValidRepositoryIsPassed()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IRepository<Genre>>();

            // Act
            var genreService = new GenreService(genreRepositoryMock.Object);

            // Assert
            Assert.IsInstanceOf<IGenreService>(genreService);
        }
    }
}
