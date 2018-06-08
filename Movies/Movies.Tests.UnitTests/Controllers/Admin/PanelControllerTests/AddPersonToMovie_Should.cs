using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Moq;

using Movies.Common;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers;
using Movies.Web.ViewModels.Admin;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Admin.PanelControllerTests
{
    [TestFixture]
    public class AddPersonToMovie_Should
    {
        [Test]
        public void RenderAddPersonToMoviePartialView_WithPeopleList()
        {
            // Arrange
            var addPersonToMoviePartialView = PartialViews.AddPersonToMovie;
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var personModel = new Person()
            {
                FirstName = "Ivan",
                LastName = "Stanev",
                Nationality = "Bulgarian",
                DateOfBirth = DateTime.UtcNow,
                Gender = Gender.Male
            };

            IEnumerable<Person> peopleList = new List<Person>() { personModel };

            personServiceMock.Setup(ps => ps.GetAllPeople()).Returns(peopleList);

            // Act
            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            panelController
                .WithCallTo(c => c.AddPersonToMovie())
                .ShouldRenderPartialView(addPersonToMoviePartialView)
                .WithModel<PersonInMovieViewModel>(viewModel =>
                {
                    Assert.AreEqual(viewModel.People.FirstOrDefault().Value, personModel.Id.ToString());
                });
        }

        [Test]
        public void RenderAddPersonToMoviePartialView_WithMoviesList()
        {
            // Arrange
            var addPersonToMoviePartialView = PartialViews.AddPersonToMovie;
            var genreServiceMock = new Mock<IGenreService>();
            var movieServiceMock = new Mock<IMovieService>();
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            var movieModel = new Movie()
            {
                Name = "Movie Name",
                Year = "2000",
                RunningTime = 120,
                Description = "Movie Description",
                Rating = 5,
                GenreId = 1
            };

            IEnumerable<Movie> moviesList = new List<Movie>() { movieModel };

            movieServiceMock.Setup(ms => ms.GetAllMovies()).Returns(moviesList);

            // Act
            var panelController = new PanelController(genreServiceMock.Object,
                movieServiceMock.Object, personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            panelController
                .WithCallTo(c => c.AddPersonToMovie())
                .ShouldRenderPartialView(addPersonToMoviePartialView)
                .WithModel<PersonInMovieViewModel>(viewModel =>
                {
                    Assert.AreEqual(viewModel.Movies.FirstOrDefault().Value, movieModel.Id.ToString());
                });
        }
    }
}
