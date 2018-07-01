using Moq;

using Movies.Services.Contracts;
using Movies.Web.Controllers;

using NUnit.Framework;

namespace Movies.Tests.UnitTests.Controllers.PersonControllerTests
{
    [TestFixture]
    public class PersonPicture_Should
    {
        [Test]
        public void ReturnDefaultPicture_WhenPictureOfPersonIsNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var personId = -1;

            personServiceMock.Setup(ps => ps.GetPersonImage(personId)).Returns((byte[])null);

            var personController = new PersonController(personServiceMock.Object, fileConverterMock.Object);

            // Act
            var result = personController.PersonPicture(personId);

            // Assert
            fileConverterMock.Verify(fc => fc.GetDefaultPicture(), Times.Once);
        }

        [Test]
        public void ReturnPersonPicture_WhenPictureOfPersonIsNotNull()
        {
            // Arrange
            var personServiceMock = new Mock<IPersonService>();
            var fileConverterMock = new Mock<IFileConverter>();
            var personId = -1;

            var personController = new PersonController(personServiceMock.Object, fileConverterMock.Object);

            // Act
            var result = personController.PersonPicture(personId);

            // Assert
            fileConverterMock.Verify(fc => fc.GetDefaultPicture(), Times.Never);
        }
    }
}
