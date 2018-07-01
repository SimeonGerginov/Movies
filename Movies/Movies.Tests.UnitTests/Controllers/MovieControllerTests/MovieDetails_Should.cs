using System.Collections.Generic;
using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Controllers;
using Movies.Web.ViewModels.MovieViewModels;
using Movies.Web.ViewModels.PersonViewModels;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.MovieControllerTests
{
    [TestFixture]
    public class MovieDetails_Should
    {
        [Test]
        public void ReturnPartialView_WithMovieDetailsViewModel()
        {
            // Arrange
            var movieServiceMock = new Mock<IMovieService>();
            var movieRoleServiceMock = new Mock<IMovieRoleService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var partialView = "_MovieDetailsPartial";

            var movie = new Movie()
            {
                Id = 1,
                Name = "Memento",
                Description = "Some description",
                Year = "1920"
            };

            var movieDetails = new MovieDetailsViewModel()
            {
                Id = movie.Id,
                Name = movie.Name,
                Year = movie.Year,
                Description = movie.Description,
                Participants = new List<MovieParticipantViewModel>()
            };

            movieServiceMock.Setup(ms => ms.GetMovie(movie.Id)).Returns(movie);
            mapperMock.Setup(m => m.Map<MovieDetailsViewModel>(movie)).Returns(movieDetails);

            // Act
            var movieController =
                new MovieController(movieServiceMock.Object,
                    movieRoleServiceMock.Object,
                    fileConverterMock.Object,
                    mapperMock.Object);

            // Assert
            movieController
                .WithCallTo(c => c.MovieDetails(movie.Id))
                .ShouldRenderPartialView(partialView)
                .WithModel<MovieDetailsViewModel>(movieDetails);
        }
    }
}
