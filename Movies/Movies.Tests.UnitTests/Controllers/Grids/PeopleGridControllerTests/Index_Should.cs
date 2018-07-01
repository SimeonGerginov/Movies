using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace Movies.Tests.UnitTests.Controllers.Grids.PeopleGridControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void ReturnDefaultView_WhenCalled()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            peopleGridController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
