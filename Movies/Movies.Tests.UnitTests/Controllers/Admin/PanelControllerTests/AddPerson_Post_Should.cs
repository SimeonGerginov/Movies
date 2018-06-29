using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;
using Movies.Web.ViewModels.Admin;
using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddPerson_Post_Should
    {
        [Test]
        public void OnlyRedirect_WhenModelStateIsValid()
        {
            // Arrange
            var personViewModel = new PersonViewModel()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(personViewModel, null, null);

            var results = new List<ValidationResult>();

            // Act
            var isModelValid = Validator.TryValidateObject(personViewModel, validationContext, results);

            // Assert
            Assert.IsFalse(isModelValid);
        }

        [Test]
        public void NotCallAddMethodOfPersonService_WhenModelStateIsInvalid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var personViewModel = new PersonViewModel()
            {
            };

            var personDbModel = new Person()
            {
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(personViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(personViewModel, validationContext, results);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            panelController.ModelState.AddModelError("name", "No person name!");

            // Act
            panelController.AddPerson(personViewModel);

            // Assert
            Assert.IsFalse(isModelValid);
            personServiceMock.Verify(ps => ps.AddPerson(personDbModel), Times.Never);
        }

        [Test]
        public void CallAddMethodOfPersonService_WhenModelStateIsValid()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var personViewModel = new PersonViewModel()
            {
                FirstName = "Ivan",
                LastName = "Stanev",
                Nationality = "Bulgarian",
                DateOfBirth = DateTime.UtcNow,
                Gender = Gender.Male
            };

            var personDbModel = new Person()
            {
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName,
                Nationality = personViewModel.Nationality,
                DateOfBirth = personViewModel.DateOfBirth,
                Gender = personViewModel.Gender
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(personViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(personViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Person>(personViewModel)).Returns(personDbModel);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            
            var contextMock = new Mock<HttpContextBase>();
            var requestMock = new Mock<HttpRequestBase>();
            var filesMock = new Mock<HttpFileCollectionBase>();

            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            filesMock.Setup(f => f.Count).Returns(0);
            requestMock.Setup(r => r.Files).Returns(filesMock.Object);
            panelController.ControllerContext = 
                new ControllerContext(contextMock.Object, new RouteData(), panelController);

            // Act
            panelController.AddPerson(personViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            personServiceMock.Verify(ps => ps.AddPerson(personDbModel), Times.Once);
        }

        [Test]
        public void CallPostedToByteArrayMethodOfFileConverter_WhenModelStateIsValidAndThereAreFilesInTheRequest()
        {
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var personViewModel = new PersonViewModel()
            {
                FirstName = "Ivan",
                LastName = "Stanev",
                Nationality = "Bulgarian",
                DateOfBirth = DateTime.UtcNow,
                Gender = Gender.Male
            };

            var personDbModel = new Person()
            {
                FirstName = personViewModel.FirstName,
                LastName = personViewModel.LastName,
                Nationality = personViewModel.Nationality,
                DateOfBirth = personViewModel.DateOfBirth,
                Gender = personViewModel.Gender
            };

            var validationContext =
                new System.ComponentModel.DataAnnotations.ValidationContext(personViewModel, null, null);

            var results = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(personViewModel, validationContext, results);

            mapperMock.Setup(x => x.Map<Person>(personViewModel)).Returns(personDbModel);

            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            var contextMock = new Mock<HttpContextBase>();
            var requestMock = new Mock<HttpRequestBase>();
            var filesMock = new Mock<HttpFileCollectionBase>();
            var pictureMock = new Mock<HttpPostedFileBase>();

            contextMock.Setup(c => c.Request).Returns(requestMock.Object);
            filesMock.Setup(f => f.Count).Returns(1);
            filesMock.Setup(f => f["Picture"]).Returns(pictureMock.Object);
            requestMock.Setup(r => r.Files).Returns(filesMock.Object);
            panelController.ControllerContext =
                new ControllerContext(contextMock.Object, new RouteData(), panelController);

            // Act
            panelController.AddPerson(personViewModel);

            // Assert
            Assert.IsTrue(isModelValid);
            fileConverterMock.Verify(fcm => fcm.PostedToByteArray(pictureMock.Object), Times.Once);
        }
    }
}
