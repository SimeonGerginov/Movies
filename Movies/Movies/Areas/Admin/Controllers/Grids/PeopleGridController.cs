using System.Linq;
using System.Web.Mvc;
using Bytes2you.Validation;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Infrastructure.Extensions;
using Movies.Services.Contracts;
using Movies.Services.Mappings;
using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.Areas.Admin.Models;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class PeopleGridController : AdminController
    {
        private readonly IPersonService personService;
        private readonly IFileConverter fileConverter;

        public PeopleGridController(IPersonService personService, IFileConverter fileConverter)
        {
            Guard.WhenArgument(personService, "Person Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();

            this.personService = personService;
            this.fileConverter = fileConverter;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadPeople([DataSourceRequest] DataSourceRequest request)
        {
            var people = this.personService
                .GetAllPeople()
                .Map<Person, GridPersonViewModel>()
                .Select(p => p.PictureFile = this.GetPersonPhoto(p))
                .ToDataSourceResult(request);

            return this.Json(people);
        }

        [SaveChanges]
        public ActionResult DeletePerson(GridPersonViewModel personModel)
        {
            if (personModel != null)
            {
                this.personService.DeletePerson(personModel.Id);
            }

            return this.Json(new[] { personModel });
        }

        [SaveChanges]
        public ActionResult EditPerson(GridPersonViewModel personModel)
        {
            if (personModel != null)
            {
                var person = MappingService.MappingProvider.Map<Person>(personModel);
                this.personService.UpdatePerson(person);
            }

            return this.Json(new[] { personModel });
        }

        public FileContentResult GetPersonPhoto(GridPersonViewModel person)
        {
            var personImage = person.Picture;

            if (personImage == null)
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();

                return this.File(defaultImage, "image/png");
            }
            else
            {
                return this.File(personImage, "image/jpeg");
            }
        }
    }
}
