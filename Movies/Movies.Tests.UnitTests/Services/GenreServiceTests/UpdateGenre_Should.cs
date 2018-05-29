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
    public class UpdateGenre_Should
    {
        [Test]
        public void NotCallUpdateMethodOfRepository_WhenPassedGenreDoesNotExist()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            IEnumerable<Genre> filteredGenres = new List<Genre>();

            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            // Act
            genreService.UpdateGenre(genreMock.Object);

            // Assert
            genreRepositoryMock.Verify(gr => gr.Update(genreMock.Object), Times.Never);
        }

        [Test]
        public void CallUpdateMethodOfRepositoryOnce_WhenPassedGenreExists()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            ICollection<Genre> filteredGenres = new List<Genre>();
            filteredGenres.Add(genreMock.Object);

            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            // Act
            genreService.UpdateGenre(genreMock.Object);

            // Assert
            genreRepositoryMock.Verify(gr => gr.Update(genreMock.Object), Times.Once);
        }
    }
}
