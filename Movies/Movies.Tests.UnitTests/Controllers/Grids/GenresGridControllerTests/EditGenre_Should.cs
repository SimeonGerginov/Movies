using System.Collections.Generic;
using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.GridViewModels;
using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.GenresGridControllerTests
{
    [TestFixture]
    public class EditGenre_Should
    {
        [Test]
        public void NotCallGenreServiceUpdateGenre_WhenPassedNullGridGenreViewModel()
        {
            // Arrange 
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);
            genresGridController.EditGenre(null);

            // Assert
            genreServiceMock.Verify(gs => gs.UpdateGenre(It.IsAny<Genre>()), Times.Never);
        }

        [Test]
        public void CallGenreServiceUpdateGenre_WhenPassedGridGenreViewModel()
        {
            // Arrange 
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();

            var gridGenreViewModel = new GridGenreViewModel()
            {
                Name = "Genre"
            };

            var genreDbModel = new Genre()
            {
                Name = gridGenreViewModel.Name
            };

            var genresList = new List<Genre>() { genreDbModel };
            genreServiceMock.Setup(gs => gs.GetAllGenres()).Returns(genresList);

            mapperMock.Setup(x => x.Map<Genre>(gridGenreViewModel)).Returns(genreDbModel);

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);
            genresGridController.EditGenre(gridGenreViewModel);

            // Assert
            genreServiceMock.Verify(gs => gs.UpdateGenre(genreDbModel), Times.Once);
        }
    }
}
