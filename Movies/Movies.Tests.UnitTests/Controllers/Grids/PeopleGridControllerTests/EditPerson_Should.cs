﻿using System;
using System.Collections.Generic;

using AutoMapper;
using Moq;

using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Grid;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.PeopleGridControllerTests
{
    [TestFixture]
    public class EditPerson_Should
    {
        [Test]
        public void NotCallPersonServiceUpdatePerson_WhenPassedNullGridPersonViewModel()
        {
            // Arrange 
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            peopleGridController.EditPerson(null);

            // Assert
            personServiceMock.Verify(ps => ps.UpdatePerson(It.IsAny<Person>()), Times.Never);
        }

        [Test]
        public void CallPersonServiceUpdatePerson_WhenPassedGridPersonViewModel()
        {
            // Arrange 
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var picture = new byte[128];

            var gridPersonViewModel = new GridPersonViewModel()
            {
                FirstName = "Person first name",
                LastName = "Person last name",
                Nationality = "Person nationality",
                Gender = Gender.Male,
                DateOfBirth = DateTime.UtcNow,
                Picture = picture
            };

            var personDbModel = new Person()
            {
                FirstName = gridPersonViewModel.FirstName,
                LastName = gridPersonViewModel.LastName,
                Nationality = gridPersonViewModel.Nationality,
                Gender = gridPersonViewModel.Gender,
                DateOfBirth = gridPersonViewModel.DateOfBirth,
                Picture = gridPersonViewModel.Picture
            };

            var peopleList = new List<Person>() { personDbModel };
            personServiceMock.Setup(ps => ps.GetAllPeople()).Returns(peopleList);

            mapperMock.Setup(x => x.Map<GridPersonViewModel>(personDbModel)).Returns(gridPersonViewModel);
            mapperMock.Setup(x => x.Map<Person>(gridPersonViewModel)).Returns(personDbModel);

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            peopleGridController.EditPerson(gridPersonViewModel);

            // Assert
            personServiceMock.Verify(ps => ps.UpdatePerson(personDbModel), Times.Once);
        }
    }
}
