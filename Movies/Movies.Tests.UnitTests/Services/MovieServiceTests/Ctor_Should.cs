using System;
using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services;
using Movies.Services.Contracts;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.MovieServiceTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void Throw_WhenPassedMovieRepositoryIsNull()
        {
            // Arrange
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new MovieService(null, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object));
        }

        [Test]
        public void Throw_WhenPassedPersonRepositoryIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new MovieService(movieRepositoryMock.Object, null,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object));
        }

        [Test]
        public void Throw_WhenPassedGenreRepositoryIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new MovieService(movieRepositoryMock.Object, 
                personRepositoryMock.Object, null, movieRoleRepositoryMock.Object));
        }

        [Test]
        public void Throw_WhenPassedMovieRoleRepositoryIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new MovieService(movieRepositoryMock.Object,
                personRepositoryMock.Object, genreRepositoryMock.Object, null));
        }

        [Test]
        public void ReturnAnInstanceOfIMovieService_WhenValidRepositoriesArePassed()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();

            // Act
            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object, 
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Assert
            Assert.IsInstanceOf<IMovieService>(movieService);
        }
    }
}
