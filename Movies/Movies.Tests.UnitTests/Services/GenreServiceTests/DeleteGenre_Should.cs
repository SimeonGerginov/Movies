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
    public class DeleteGenre_Should
    {
        [Test]
        public void ReturnFalse_WhenPassedGenreDoesNotExist()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            IEnumerable<Genre> filteredGenres = new List<Genre>();
            
            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            // Act
            var result = genreService.DeleteGenre(genreMock.Object.Name);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CallDeleteMethodOfRepositoryOnce_WhenPassedGenreExists()
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
            genreService.DeleteGenre(genreMock.Object.Name);

            // Assert
            genreRepositoryMock.Verify(gr => gr.Delete(genreMock.Object), Times.Once);
        }

        [Test]
        public void ReturnTrue_WhenPassedGenreExists()
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
            var result = genreService.DeleteGenre(genreMock.Object.Name);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
