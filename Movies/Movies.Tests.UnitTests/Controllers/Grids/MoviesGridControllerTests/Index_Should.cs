using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Grids.MoviesGridControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            moviesGridController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
