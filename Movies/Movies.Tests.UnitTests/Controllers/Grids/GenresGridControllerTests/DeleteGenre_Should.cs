using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.ViewModels.GridViewModels;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.GenresGridControllerTests
{
    [TestFixture]
    public class DeleteGenre_Should
    {
        [Test]
        public void NotCallGenreServiceDeleteGenre_WhenPassedNullGridGenreViewModel()
        {
            // Arrange 
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);
            genresGridController.DeleteGenre(null);

            // Assert
            genreServiceMock.Verify(gs => gs.DeleteGenre(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void CallGenreServiceDeleteGenre_WhenPassedGridGenreViewModel()
        {
            // Arrange 
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            var gridGenreViewModel = new GridGenreViewModel()
            {
                Name = "Genre"
            };

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);
            genresGridController.DeleteGenre(gridGenreViewModel);

            // Assert
            genreServiceMock.Verify(gs => gs.DeleteGenre(gridGenreViewModel.Name), Times.Once);
        }
    }
}
