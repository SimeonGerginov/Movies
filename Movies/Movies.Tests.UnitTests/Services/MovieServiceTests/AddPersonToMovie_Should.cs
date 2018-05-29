using System;
using System.Collections.Generic;

using Moq;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Services.MovieServiceTests
{
    [TestFixture]
    public class AddPersonToMovie_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPersonIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            Person person = null;
            Role role = Role.Actor;

            personRepositoryMock.Setup(pr => pr.GetById(It.IsAny<int>())).Returns(person);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => movieService
                .AddPersonToMovie(It.IsAny<int>(), It.IsAny<int>(), role));
        }

        [Test]
        public void ThrowArgumentNullException_WhenMovieIsNull()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var personMock = new Mock<Person>();
            Movie movie = null;
            Role role = Role.Actor;

            personRepositoryMock.Setup(pr => pr.GetById(It.IsAny<int>())).Returns(personMock.Object);
            movieRepositoryMock.Setup(mr => mr.GetById(It.IsAny<int>())).Returns(movie);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => movieService
                .AddPersonToMovie(It.IsAny<int>(), It.IsAny<int>(), role));
        }

        [Test]
        public void NotAddPersonToMovie_WhenPersonIsAlreadyAddedToMovie()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var personMock = new Mock<Person>();
            var movieMock = new Mock<Movie>();
            Role role = Role.Actor;

            ICollection<Person> peopleInMovie = new List<Person>();
            peopleInMovie.Add(personMock.Object);

            movieMock.Setup(m => m.People).Returns(peopleInMovie);
            personRepositoryMock.Setup(pr => pr.GetById(It.IsAny<int>())).Returns(personMock.Object);
            movieRepositoryMock.Setup(mr => mr.GetById(It.IsAny<int>())).Returns(movieMock.Object);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            movieService.AddPersonToMovie(It.IsAny<int>(), It.IsAny<int>(), role);

            // Assert
            movieRepositoryMock.Verify(mr => mr.Update(movieMock.Object), Times.Never);
        }

        [Test]
        public void CallUpdateMethodOfMovieRepositoryOnce_WhenPersonIsNotAddedToMovie()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var personMock = new Mock<Person>();
            var movieMock = new Mock<Movie>();
            Role role = Role.Actor;

            ICollection<Person> peopleInMovie = new List<Person>();

            movieMock.Setup(m => m.People).Returns(peopleInMovie);
            personRepositoryMock.Setup(pr => pr.GetById(It.IsAny<int>())).Returns(personMock.Object);
            movieRepositoryMock.Setup(mr => mr.GetById(It.IsAny<int>())).Returns(movieMock.Object);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            movieService.AddPersonToMovie(It.IsAny<int>(), It.IsAny<int>(), role);

            // Assert
            movieRepositoryMock.Verify(mr => mr.Update(movieMock.Object), Times.Once);
        }

        [Test]
        public void CallAddMethodOfMovieRoleRepositoryOnce_WhenPersonIsNotAddedToMovie()
        {
            // Arrange
            var movieRepositoryMock = new Mock<IRepository<Movie>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var genreRepositoryMock = new Mock<IRepository<Genre>>();
            var movieRoleRepositoryMock = new Mock<IRepository<MovieRole>>();
            var personMock = new Mock<Person>();
            var movieMock = new Mock<Movie>();
            Role role = Role.Actor;

            ICollection<Person> peopleInMovie = new List<Person>();

            movieMock.Setup(m => m.People).Returns(peopleInMovie);
            personRepositoryMock.Setup(pr => pr.GetById(It.IsAny<int>())).Returns(personMock.Object);
            movieRepositoryMock.Setup(mr => mr.GetById(It.IsAny<int>())).Returns(movieMock.Object);

            var movieService = new MovieService(movieRepositoryMock.Object, personRepositoryMock.Object,
                genreRepositoryMock.Object, movieRoleRepositoryMock.Object);

            // Act
            movieService.AddPersonToMovie(It.IsAny<int>(), It.IsAny<int>(), role);

            // Assert
            movieRoleRepositoryMock.Verify(mrr => mrr.Add(It.IsAny<MovieRole>()), Times.Once);
        }
    }
}
