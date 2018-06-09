using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;
using Movies.Web.ViewModels.Admin;
using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddMovie_Post_Should
    {
        [Test]
        public void OnlyRedirect_WhenModelStateIsValid()
        {
            // Arrange
            var movieViewModel = new MovieViewModel()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(movieViewModel, null, null);

            var results = new List<ValidationResult>();

            // Act
            var isModelValid = Validator.TryValidateObject(movieViewModel, validationContext, results);

            // Assert
            Assert.IsFalse(isModelValid);
        }

        [Test]
        public void CallAddMethodOfMovieService_WhenModelStateIsValid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var movieViewModel = new MovieViewModel()
            {
                Name = "Movie Name",
                Year = "2000",
                RunningTime = 120,
                Description = "Movie Description",
                GenreName = "Genre Name"
            };

            var movieDbModel = new Movie()
            {
                Name = movieViewModel.Name,
                Year = movieViewModel.Year,
                RunningTime = movieViewModel.RunningTime,
                Description = movieViewModel.Description,
                GenreId = 1
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(movieViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(movieViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Movie>(movieViewModel)).Returns(movieDbModel);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Act
            panelController.AddMovie(movieViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            movieServiceMock.Verify(ms => ms.AddMovie(movieDbModel, movieViewModel.GenreName), Times.Once);
        }

        [Test]
        public void NotCallAddMethodOfMovieService_WhenModelStateIsInvalid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var movieViewModel = new MovieViewModel()
            {
                GenreName = "Genre Name"
            };

            var movieDbModel = new Movie()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(movieViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(movieViewModel, validationContext, results);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            panelController.ModelState.AddModelError("name", "No movie name!");

            // Act
            panelController.AddMovie(movieViewModel);

            // Assert
            Assert.IsFalse(isModelValid);
            movieServiceMock.Verify(ms => ms.AddMovie(movieDbModel, movieViewModel.GenreName), Times.Never);
        }
    }
}
