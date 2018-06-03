using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Grids.GenresGridControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);

            // Assert
            genresGridController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
