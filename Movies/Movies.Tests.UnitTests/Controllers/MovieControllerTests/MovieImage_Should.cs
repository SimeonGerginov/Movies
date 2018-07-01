using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.MovieControllerTests
{
    [TestFixture]
    public class MovieImage_Should
    {
        [Test]
        public void ReturnDefaultImage_WhenFoundImageIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var movieId = 1;

            movieServiceMock.Setup(ms => ms.GetMovieImage(movieId)).Returns((byte[])null);

            var movieController =
                new MovieController(
                    movieServiceMock.Object,
                    movieRoleServiceMock.Object,
                    fileConverterMock.Object,
                    mapperMock.Object);

            // Act
            movieController.MovieImage(movieId);

            // Assert
            fileConverterMock.Verify(fcm => fcm.GetDefaultPicture(), Times.Once);
        }

        [Test]
        public void ReturnImage_WhenFoundImageIsNotNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var image = new byte[128];
            var movieId = 1;

            movieServiceMock.Setup(ms => ms.GetMovieImage(movieId)).Returns(image);

            var movieController =
                new MovieController(
                    movieServiceMock.Object,
                    movieRoleServiceMock.Object,
                    fileConverterMock.Object,
                    mapperMock.Object);

            // Act
            movieController.MovieImage(movieId);

            // Assert
            fileConverterMock.Verify(fcm => fcm.GetDefaultPicture(), Times.Never);
        }
    }
}
