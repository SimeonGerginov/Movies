using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.GenresGridControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedGenreServiceIsNull()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new GenresGridController(null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new GenresGridController(genreServiceMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfGenresGridController_WhenPassedArgumentsAreNotNull()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<GenresGridController>(genresGridController);
        }
    }
}
