using System.Collections.Generic;
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
using WebGrease.Css.Extensions;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class PeopleGridController : AdminController
    {
        private readonly IPersonService personService;
        private readonly IFileConverter fileConverter;

        private IEnumerable<GridPersonViewModel> people;
        private IDictionary<int, FileContentResult> pictures;

        public PeopleGridController(IPersonService personService, IFileConverter fileConverter)
        {
            Guard.WhenArgument(personService, "Person Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();

            this.personService = personService;
            this.fileConverter = fileConverter;

            this.pictures = new Dictionary<int, FileContentResult>();
            this.GetPeople();
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadPeople([DataSourceRequest] DataSourceRequest request)
        {
            var people = this.people.ToDataSourceResult(request);

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

        public FileContentResult GetPhoto(string id)
        {
            return this.pictures[int.Parse(id)];
        }

        private void GetPeople()
        {
            this.people = this.personService
                .GetAllPeople()
                .Map<Person, GridPersonViewModel>();

            this.people.ForEach(p => p.PictureFile = this.SetPersonPhoto(p));
        }

        private FileContentResult SetPersonPhoto(GridPersonViewModel person)
        {
            var personImage = person.Picture;

            if (personImage == null)
            {
                var defaultImage = this.fileConverter.GetDefaultPicture();
                var file = this.File(defaultImage, "image/png");

                this.pictures.Add(person.Id, file);

                return file;
            }
            else
            {
                var file = this.File(personImage, "image/jpeg");
                this.pictures.Add(person.Id, file);

                return file;
            }
        }
    }
}
