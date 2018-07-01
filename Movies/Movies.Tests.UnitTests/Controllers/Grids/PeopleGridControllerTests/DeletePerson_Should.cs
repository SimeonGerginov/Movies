using System;
using AutoMapper;
using Moq;

using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Grid;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.PeopleGridControllerTests
{
    [TestFixture]
    public class DeletePerson_Should
    {
        [Test]
        public void NotCallPersonServiceDeletePerson_WhenPassedNullGridPersonViewModel()
        {
            // Arrange 
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            peopleGridController.DeletePerson(null);

            // Assert
            personServiceMock.Verify(ps => ps.DeletePerson(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void CallPersonServiceDeletePerson_WhenPassedGridPersonViewModel()
        {
            // Arrange 
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var gridPersonViewModel = new GridPersonViewModel()
            {
                Id = 1,
                FirstName = "Person first name",
                LastName = "Person last name",
                Nationality = "Person nationality",
                Gender = Gender.Male,
                DateOfBirth = DateTime.UtcNow,
                Picture = null
            };

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            peopleGridController.DeletePerson(gridPersonViewModel);

            // Assert
            personServiceMock.Verify(ps => ps.DeletePerson(gridPersonViewModel.Id), Times.Once);
        }
    }
}
