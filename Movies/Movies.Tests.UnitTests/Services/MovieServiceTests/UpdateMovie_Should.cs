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
    public class UpdateMovie_Should
    {
        [Test]
        public void NotCallUpdateMethodOfMovieRepository_WhenMovieDoesNotExist()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieMock = new Mock<Movie>();

            IEnumerable<Movie> filteredMovies = new List<Movie>();

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            movieService.UpdateMovie(movieMock.Object);

            // Assert
            movieRepositoryMock.Verify(mr => mr.Update(movieMock.Object), Times.Never);
        }

        [Test]
        public void CallUpdateMethodOfMovieRepositoryOnce_WhenMovieExists()
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
            movieService.UpdateMovie(movieMock.Object);

            // Assert
            movieRepositoryMock.Verify(mr => mr.Update(movieMock.Object), Times.Once);
        }
    }
}
