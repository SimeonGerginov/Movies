using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;
using Kendo.Mvc.UI;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Grid;
using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.GenresGridControllerTests
{
    [TestFixture]
    public class ReadGenres_Should
    {
        [Test]
        public void ReturnJsonContainingGenres()
        {
            // Arrange
            var genreServiceMock = new Mock<IGenreService>();
            var mapperMock = new Mock<IMapper>();
            var dataSourceRequest = new DataSourceRequest();

            var genreDbModel = new Genre()
            {
                Name = "Genre"
            };

            var gridGenreViewModel = new GridGenreViewModel()
            {
                Name = genreDbModel.Name
            };

            var genresList = new List<Genre>() { genreDbModel };
            genreServiceMock.Setup(gs => gs.GetAllGenres()).Returns(genresList);

            mapperMock.Setup(x => x.Map<GridGenreViewModel>(genreDbModel)).Returns(gridGenreViewModel);

            // Act
            var genresGridController = new GenresGridController(genreServiceMock.Object, mapperMock.Object);
            var jsonResult = genresGridController.ReadGenres(dataSourceRequest) as JsonResult;

            var dataSourceResult = jsonResult.Data as DataSourceResult;
            var dataEnumerator = dataSourceResult.Data.GetEnumerator();
            dataEnumerator.MoveNext();

            // Assert
            Assert.AreSame(dataEnumerator.Current, gridGenreViewModel);
        }
    }
}
