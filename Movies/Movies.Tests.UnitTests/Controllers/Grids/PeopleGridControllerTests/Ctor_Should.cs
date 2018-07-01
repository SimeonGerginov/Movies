using System;
using AutoMapper;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Grids;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.Grids.PeopleGridControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedPersonServiceIsNull()
        {
            // Arrange
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new PeopleGridController(null, fileConverterMock.Object, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var mapperMock = new Mock<IMapper>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new PeopleGridController(personServiceMock.Object, null, mapperMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedMapperIsNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() =>
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, null));
        }

        [Test]
        public void CreateAnInstanceOfPeopleGridController_WhenPassedArgumentsAreNotNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var mapperMock = new Mock<IMapper>();

            // Act
            var peopleGridController =
                new PeopleGridController(personServiceMock.Object, fileConverterMock.Object, mapperMock.Object);

            // Assert
            Assert.IsInstanceOf<PeopleGridController>(peopleGridController);
        }
    }
}
