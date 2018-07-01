using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Grid;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.MoviesGridControllerTests
{
    [TestFixture]
    public class DeleteMovie_Should
    {
        [Test]
        public void NotCallMovieServiceDeleteMovie_WhenPassedNullGridMovieViewModel()
        {
            // Arrange 
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            moviesGridController.DeleteMovie(null);

            // Assert
            movieServiceMock.Verify(ms => ms.DeleteMovie(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void CallMovieServiceDeleteMovie_WhenPassedGridMovieViewModel()
        {
            // Arrange 
            var movieServiceMock = new Mock<IMovieService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var gridMovieViewModel = new GridMovieViewModel()
            {
                Id = 1,
                Name = "Movie name",
                RunningTime = 120,
                Year = "1980",
                Image = null,
                GenreName = "Genre name"
            };

            // Act
            var moviesGridController =
                new MoviesGridController(movieServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            moviesGridController.DeleteMovie(gridMovieViewModel);

            // Assert
            movieServiceMock.Verify(ms => ms.DeleteMovie(gridMovieViewModel.Id), Times.Once);
        }
    }
}
