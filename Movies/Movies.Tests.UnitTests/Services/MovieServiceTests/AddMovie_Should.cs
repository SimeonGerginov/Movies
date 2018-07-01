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
    public class AddMovie_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedMovieIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();

            var movieService = new MovieService(
                movieRepositoryMock.Object, 
                personRepositoryMock.Object,
                genreRepositoryMock.Object, 
                movieRoleRepositoryMock.Object, 
                movieRatingRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => movieService.AddMovie(null, string.Empty));
        }

        [Test]
        public void ThrowInvalidOperationException_WhenPassedMovieExists()
        {
            // Arrange
            var movieMock = new Mock<Movie>();
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();

            ICollection<Movie> filteredMovies = new List<Movie>();
            filteredMovies.Add(movieMock.Object);

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);

            var movieService = new MovieService(
                movieRepositoryMock.Object, 
                personRepositoryMock.Object,
                genreRepositoryMock.Object, 
                movieRoleRepositoryMock.Object, 
                movieRatingRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => movieService.AddMovie(movieMock.Object, string.Empty));
        }

        [Test]
        public void ThrowArgumentNullException_WhenGenreDoesNotExist()
        {
            // Arrange
            var movieMock = new Mock<Movie>();
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();

            IEnumerable<Movie> filteredMovies = new List<Movie>();
            IEnumerable<Genre> filteredGenres = new List<Genre>();

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);
            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            var movieService = new MovieService(
                movieRepositoryMock.Object, 
                personRepositoryMock.Object,
                genreRepositoryMock.Object, 
                movieRoleRepositoryMock.Object, 
                movieRatingRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => movieService.AddMovie(movieMock.Object, string.Empty));
        }

        [Test]
        public void CallAddMethodOfMovieRepositoryOnce()
        {
            // Arrange
            var movieMock = new Mock<Movie>();
            var genreMock = new Mock<Genre>();
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();

            IEnumerable<Movie> filteredMovies = new List<Movie>();
            ICollection<Genre> filteredGenres = new List<Genre>();

            filteredGenres.Add(genreMock.Object);

            movieRepositoryMock.Setup(mr => mr.GetAllFiltered(It.IsAny<Expression<Func<Movie, bool>>>()))
                .Returns(filteredMovies);
            genreRepositoryMock.Setup(gr => gr.GetAllFiltered(It.IsAny<Expression<Func<Genre, bool>>>()))
                .Returns(filteredGenres);

            var movieService = new MovieService(
                movieRepositoryMock.Object, 
                personRepositoryMock.Object,
                genreRepositoryMock.Object, 
                movieRoleRepositoryMock.Object, 
                movieRatingRepositoryMock.Object);

            // Act
            movieService.AddMovie(movieMock.Object, string.Empty);

            // Assert
            movieRepositoryMock.Verify(mr => mr.Add(movieMock.Object), Times.Once);
        }
    }
}
