using AutoMapper;
using Moq;

using Movies.Common;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddGenre_Should
    {
        [Test]
        public void RenderAddGenrePartialView()
        {
            // Arrange
            var addGenrePartialView = PartialViews.AddGenre;
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            panelController
                .WithCallTo(c => c.AddGenre())
                .ShouldRenderPartialView(addGenrePartialView);
        }
    }
}
