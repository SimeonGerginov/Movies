using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedGenreServiceIsNull()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(null, movieServiceMock.Object,
                personServiceMock.Object, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMovieServiceIsNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(genreServiceMock.Object, null, 
                personServiceMock.Object, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedPersonServiceIsNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(genreServiceMock.Object, 
                movieServiceMock.Object, null, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfPanelController_WhenValidServicesArePassed()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<PanelController>(panelController);
        }
    }
}
