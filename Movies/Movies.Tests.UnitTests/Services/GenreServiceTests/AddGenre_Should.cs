using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.GenreServiceTests
{
    [TestFixture]
    public class AddGenre_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedGenreIsNull()
        {
            // Arrange
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => genreService.AddGenre(null));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenPassedGenreExists()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            ICollection<Genre> filteredGenres = new List<Genre>();
            filteredGenres.Add(genreMock.Object);

            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => genreService.AddGenre(genreMock.Object));
        }

        [Test]
        public void CallAddMethodOfRepositoryOnce_WhenPassedGenreDoesNotExists()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            IEnumerable<Genre> filteredGenres = new List<Genre>();

            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            // Act
            genreService.AddGenre(genreMock.Object);

            // Assert
            genreRepositoryMock.Verify(gr => gr.Add(genreMock.Object), Times.Once);
        }
    }
}
