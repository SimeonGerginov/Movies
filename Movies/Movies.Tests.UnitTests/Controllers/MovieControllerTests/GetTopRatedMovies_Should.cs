using System.Collections.Generic;
using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Controllers;
using Movies.Web.ViewModels.MovieViewModels;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.MovieControllerTests
{
    [TestFixture]
    public class GetTopRatedMovies_Should
    {
        [Test]
        public void ReturnView_WithTopRatedMovies()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var partialView = "_TopRatedMoviesPartial";

            var movie = new Movie()
            {
                Name = "Memento",
                Description = "Some description",
                Year = "1920"
            };

            var topRatedMovie = new TopRatedMovieViewModel()
            {
                Name = movie.Name,
                Year = int.Parse(movie.Year)
            };

            ICollection<Movie> topMovies = new List<Movie>();
            topMovies.Add(movie);

            movieServiceMock.Setup(ms => ms.GetTopRatedMovies(It.IsAny<int>())).Returns(topMovies);
            mapperMock.Setup(m => m.Map<TopRatedMovieViewModel>(movie)).Returns(topRatedMovie);

            // Act
            var movieController =
                new MovieController(movieServiceMock.Object,
                    movieRoleServiceMock.Object,
                    fileConverterMock.Object,
                    mapperMock.Object);

            // Assert
            movieController
                .WithCallTo(c => c.GetTopRatedMovies())
                .ShouldRenderPartialView(partialView)
                .WithModel<IEnumerable<TopRatedMovieViewModel>>();
        }
    }
}
