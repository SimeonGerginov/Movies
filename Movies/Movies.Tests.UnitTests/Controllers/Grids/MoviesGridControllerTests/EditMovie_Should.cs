using System.Collections.Generic;
using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Grid;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.MoviesGridControllerTests
{
    [TestFixture]
    public class EditMovie_Should
    {
        [Test]
        public void NotCallMovieServiceUpdateMovie_WhenPassedNullGridMovieViewModel()
        {
            // Arrange 
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            moviesGridController.EditMovie(null);

            // Assert
            movieServiceMock.Verify(ms => ms.UpdateMovie(It.IsAny<Movie>()), Times.Never);
        }

        [Test]
        public void CallMovieServiceUpdateMovie_WhenPassedGridMovieViewModel()
        {
            // Arrange 
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var image = new byte[128];

            var gridMovieViewModel = new GridMovieViewModel()
            {
                Id = 1,
                Name = "Movie name",
                Image = image,
                Year = "1980",
                RunningTime = 120,
                GenreName = "Genre name"
            };

            var movieDbModel = new Movie()
            {
                Name = gridMovieViewModel.Name,
                Image = gridMovieViewModel.Image,
                Year = gridMovieViewModel.Year,
                RunningTime = gridMovieViewModel.RunningTime
            };

            var moviesList = new List<Movie>() { movieDbModel };
            movieServiceMock.Setup(ms => ms.GetAllMovies()).Returns(moviesList);

            mapperMock.Setup(x => x.Map<Movie>(gridMovieViewModel)).Returns(movieDbModel);
            mapperMock.Setup(x => x.Map<GridMovieViewModel>(movieDbModel)).Returns(gridMovieViewModel);

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            moviesGridController.EditMovie(gridMovieViewModel);

            // Assert
            movieServiceMock.Verify(ms => ms.UpdateMovie(movieDbModel), Times.Once);
        }
    }
}
