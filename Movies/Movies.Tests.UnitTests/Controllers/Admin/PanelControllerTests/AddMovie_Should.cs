using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Moq;

using Movies.Common;
using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;
using Movies.Web.ViewModels.Admin;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddMovie_Should
    {
        [Test]
        public void RenderAddMoviePartialView()
        {
            // Arrange
            var addMoviePartialView = PartialViews.AddMovie;
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var genreModel = new Genre()
            {
                Name = "Genre name"
            };

            IEnumerable<Genre> genreList = new List<Genre>() { genreModel };

            genreServiceMock.Setup(gs => gs.GetAllGenres()).Returns(genreList);

            // Act
            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            panelController
                .WithCallTo(c => c.AddMovie())
                .ShouldRenderPartialView(addMoviePartialView)
                .WithModel<MovieViewModel>(viewModel =>
                {
                    Assert.AreEqual(viewModel.GenresSelectList.FirstOrDefault().Text, genreModel.Name);
                });
        }
    }
}
