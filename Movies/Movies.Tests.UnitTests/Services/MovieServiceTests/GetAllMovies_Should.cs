using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.MovieServiceTests
{
    [TestFixture]
    public class GetAllMovies_Should
    {
        [Test]
        public void CallGetAllAndIncludeEntityOnce()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();
            string genreEntity = "Genre";

            var movieService = new MovieService(
                movieRepositoryMock.Object, 
                personRepositoryMock.Object,
                genreRepositoryMock.Object, 
                movieRoleRepositoryMock.Object, 
                movieRatingRepositoryMock.Object);

            // Act
            var result = movieService.GetAllMovies();

            // Assert
            movieRepositoryMock.Verify(mr => mr.GetAllAndIncludeEntity(genreEntity), Times.Once);
        }
    }
}
