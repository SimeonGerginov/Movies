using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.MovieControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedMovieServiceIsNull()
        {
            // Arrange
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MovieController(null, movieRoleServiceMock.Object, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMovieRoleServiceIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MovieController(movieServiceMock.Object, null, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MovieController(movieServiceMock.Object, movieRoleServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MovieController(
                    movieServiceMock.Object, 
                    movieRoleServiceMock.Object, 
                    fileConverterMock.Object, 
                    null));
        }

        [Test]
        public void CreateAnInstanceOfMovieController_WhenAllParametersAreValid()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var movieController = 
                new MovieController(
                    movieServiceMock.Object,
                    movieRoleServiceMock.Object,
                    fileConverterMock.Object,
                    mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<MovieController>(movieController);
        }
    }
}
