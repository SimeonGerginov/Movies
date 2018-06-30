using System;
using System.Collections.Generic;
using System.Web.Mvc;

using AutoMapper;
using Kendo.Mvc.UI;
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
    public class ReadPeople_Should
    {
        [Test]
        public void ReturnJsonContainingPeople()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();
            var dataSourceRequest = new DataSourceRequest();

            var personDbModel = new Person()
            {
                FirstName = "Person first name",
                LastName = "Person last name",
                Nationality = "Person nationality",
                Gender = Gender.Male,
                DateOfBirth = DateTime.UtcNow,
                Picture = null
            };

            var gridPersonViewModel = new GridPersonViewModel()
            {
                FirstName = personDbModel.FirstName,
                LastName = personDbModel.LastName,
                Nationality = personDbModel.Nationality,
                Gender = personDbModel.Gender,
                DateOfBirth = personDbModel.DateOfBirth,
                Picture = personDbModel.Picture
            };

            var peopleList = new List<Person>() { personDbModel };
            personServiceMock.Setup(ps => ps.GetAllPeople()).Returns(peopleList);

            mapperMock.Setup(x => x.Map<GridPersonViewModel>(personDbModel)).Returns(gridPersonViewModel);

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);
            var jsonResult = peopleGridController.ReadPeople(dataSourceRequest) as JsonResult;

            var dataSourceResult = jsonResult.Data as DataSourceResult;
            var dataEnumerator = dataSourceResult.Data.GetEnumerator();
            dataEnumerator.MoveNext();

            // Assert
            Assert.AreSame(dataEnumerator.Current, gridPersonViewModel);
        }
    }
}
