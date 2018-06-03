using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Services.Contracts;
using Movies.ViewModels.AdminViewModels;
using Movies.Web.Areas.Admin.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddGenre_Post_Should
    {
        [Test]
        public void OnlyRedirect_WhenModelStateIsValid()
        {
            // Arrange
            var genreViewModel = new GenreViewModel()
            {
            };

            var validationContext = 
                new System.ComponentModel.DataAnnotations.ValidationContext(genreViewModel, null, null);

            var results = new List<ValidationResult>();

            // Act
            var isModelValid = Validator.TryValidateObject(genreViewModel, validationContext, results);

            // Assert
            Assert.IsFalse(isModelValid);
        }

        [Test]
        public void CallAddMethodOfGenreService_WhenModelStateIsValid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var genreViewModel = new GenreViewModel()
            {
                Name = "Genre"
            };

            var genreDbModel = new Genre()
            {
                Name = genreViewModel.Name
            };

            var validationContext = 
                new System.ComponentModel.DataAnnotations.ValidationContext(genreViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(genreViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Genre>(genreViewModel)).Returns(genreDbModel);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Act
            panelController.AddGenre(genreViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            genreServiceMock.Verify(gs => gs.AddGenre(genreDbModel), Times.Once);
        }

        [Test]
        public void NotCallAddMethodOfGenreService_WhenModelStateIsInvalid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var genreViewModel = new GenreViewModel()
            {
            };

            var genreDbModel = new Genre()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(genreViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(genreViewModel, validationContext, results);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            panelController.ModelState.AddModelError("name", "No genre name!");

            // Act
            panelController.AddGenre(genreViewModel);

            // Assert
            Assert.IsFalse(isModelValid);
            genreServiceMock.Verify(gs => gs.AddGenre(genreDbModel), Times.Never);
        }
    }
}
