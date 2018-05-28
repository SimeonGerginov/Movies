using System.Collections.Generic;
using System.Linq;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.GenreServiceTests
{
    [TestFixture]
    public class GetAllGenres_Should
    {
        [Test]
        public void ReturnAllGenresFromRepository()
        {
            // Arrange
            var genreMock = new Mock<Genre>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var genreService = new GenreService(genreRepositoryMock.Object);

            ICollection<Genre> genresFromRepository = new List<Genre>();
            genresFromRepository.Add(genreMock.Object);

            genreRepositoryMock.Setup(gr => gr.GetAll())
                .Returns(genresFromRepository);

            // Act
            var genres = genreService.GetAllGenres();

            // Assert
            Assert.AreEqual(genres.Count(), genresFromRepository.Count);
            CollectionAssert.Contains(genres, genreMock.Object);
        }
    }
}
