using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.MovieServiceTests
{
    [TestFixture]
    public class DeleteMovie_Should
    {
        [Test]
        public void ReturnFalse_WhenThePassedMovieDoesNotExist()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();

            IEnumerable<Movie> filteredMovies = new List<Movie>();

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            var result = movieService.DeleteMovie(It.IsAny<int>());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CallDeleteMethodOfMovieRepositoryOnce()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieMock = new Mock<Movie>();

            ICollection<Movie> filteredMovies = new List<Movie>();
            filteredMovies.Add(movieMock.Object);

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            movieService.DeleteMovie(It.IsAny<int>());

            // Assert
            movieRepositoryMock.Verify(mr => mr.Delete(movieMock.Object), Times.Once);
        }

        [Test]
        public void ReturnTrue_WhenTheMovieExistsInTheRepository()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieMock = new Mock<Movie>();

            ICollection<Movie> filteredMovies = new List<Movie>();
            filteredMovies.Add(movieMock.Object);

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            var result = movieService.DeleteMovie(It.IsAny<int>());

            // Assert
            Assert.IsTrue(result);
        }
    }
}
