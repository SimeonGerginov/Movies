using System;
using Moq;

using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.PersonControllerTests
{
    [TestFixture]
    public class Ctor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPassedPersonServiceIsNull()
        {
            // Arrange
            var fileConverterMock = new Mock<IFileConverter>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PersonController(null, fileConverterMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenPassedFileConverterIsNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new PersonController(personServiceMock.Object, null));
        }

        [Test]
        public void ReturnAnInstanceOfPersonController_WhenValidArgumentsArePassed()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();

            // Act
            var personController = new PersonController(personServiceMock.Object, fileConverterMock.Object);

            // Assert
            Assert.IsInstanceOf<PersonController>(personController);
        }
    }
}
