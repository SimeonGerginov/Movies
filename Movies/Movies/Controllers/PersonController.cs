using System.Web.Mvc;
using Bytes2you.Validation;
using Movies.Services.Contracts;

namespace Movies.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService personService;
        private readonly IFileConverter fileConverter;

        public PersonController(IPersonService personService, IFileConverter fileConverter)
        {
            Guard.WhenArgument(personService, "Person Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();

            this.personService = personService;
            this.fileConverter = fileConverter;
        }

        public ActionResult PersonPicture(int personId)
        {
            var picture = this.personService.GetPersonImage(personId);
            FileContentResult file = null;

            if (picture == null)
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                file = this.File(defaultImage, "image/png");

                return file;
            }
            else
            {
                file = this.File(picture, "image/jpeg");

                return file;
            }
        }
    }
}
