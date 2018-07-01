using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.MovieServiceTests
{
    [TestFixture]
    public class GetMovieImage_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenMovieWithThePassedIdDoesNotExist()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();
            var movieId = -1;

            // Act
            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object, movieRatingRepositoryMock.Object);
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => movieService.GetMovieImage(movieId));
        }

        [Test]
        public void ReturnMovieImage_WhenMovieWithThePassedIdExists()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var movieRatingRepositoryMock = new Mock<IRepository<MovieRating>>();
            var image = new byte[128];

            var movie = new Movie()
            {
                Id = 1,
                Name = "Movie name",
                Image = image,
                Year = "1980",
                RunningTime = 120
            };

            movieRepositoryMock.Setup(mr => mr.GetById(movie.Id)).Returns(movie);

            // Act
            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object, movieRatingRepositoryMock.Object);
            var movieImage = movieService.GetMovieImage(movie.Id);

            // Assert
            Assert.AreEqual(movieImage, movie.Image);
        }
    }
}
