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

namespace Movies.Tests.UnitTests.Controllers.Grids.MoviesGridControllerTests
{
    [TestFixture]
    public class ReadMovies_Should
    {
        [Test]
        public void ReturnJsonContainingGenres()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var dataSourceRequest = new DataSourceRequest();

            var movieDbModel = new Movie()
            {
                Name = "Movie name",
                Description = "Movie description",
                RunningTime = 120,
                Year = "1980",
                Image = null,
                GenreId = 1
            };

            var gridMovieViewModel = new GridMovieViewModel()
            {
                Name = movieDbModel.Name,
                Image = movieDbModel.Image,
                Year = movieDbModel.Year,
                RunningTime = movieDbModel.RunningTime,
                GenreName = "Genre name"
            };

            var moviesList = new List<Movie>() { movieDbModel };
            movieServiceMock.Setup(ms => ms.GetAllMovies()).Returns(moviesList);

            mapperMock.Setup(x => x.Map<GridMovieViewModel>(movieDbModel)).Returns(gridMovieViewModel);

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            var jsonResult = moviesGridController.ReadMovies(dataSourceRequest) as JsonResult;

            var dataSourceResult = jsonResult.Data as DataSourceResult;
            var dataEnumerator = dataSourceResult.Data.GetEnumerator();
            dataEnumerator.MoveNext();

            // Assert
            Assert.AreSame(dataEnumerator.Current, gridMovieViewModel);
        }
    }
}
