using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.MoviesGridControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedMovieServiceIsNull()
        {
            // Arrange
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MoviesGridController(null, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MoviesGridController(movieServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfMoviesGridController_WhenPassedArgumentsAreNotNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var moviesGridController = 
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<MoviesGridController>(moviesGridController);
        }
    }
}
