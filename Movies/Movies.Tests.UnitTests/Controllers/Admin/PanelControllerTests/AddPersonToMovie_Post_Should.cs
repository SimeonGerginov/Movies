using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AutoMapper;
using Moq;

using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;
using Movies.Web.ViewModels.AdminViewModels;
using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddPersonToMovie_Post_Should
    {
        [Test]
        public void CallAddPersonToMovieMethodOfMovieService()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var personInMovieViewModel = new PersonInMovieViewModel()
            {
                MovieId = 1,
                PersonId = 1,
                Role = Role.Actor
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(personInMovieViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(personInMovieViewModel, validationContext, results);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Act
            panelController.AddPersonToMovie(personInMovieViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            movieServiceMock.Verify(ms => ms.AddPersonToMovie(
                personInMovieViewModel.MovieId, 
                personInMovieViewModel.PersonId, 
                personInMovieViewModel.Role), Times.Once);
        }
    }
}
