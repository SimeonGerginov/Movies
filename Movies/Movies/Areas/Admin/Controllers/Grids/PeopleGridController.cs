using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using Bytes2you.Validation;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Movies.Core.Models;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.ViewModels.GridViewModels;
using Movies.Web.Areas.Admin.Controllers.Abstraction;

using WebGrease.Css.Extensions;

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class PeopleGridController : AdminController
    {
        private readonly IPersonService personService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        private IEnumerable<GridPersonViewModel> people;
        private IDictionary<int, FileContentResult> pictures;

        public PeopleGridController(IPersonService personService, IFileConverter fileConverter, IMapper mapper)
        {
            Guard.WhenArgument(personService, "Person Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.personService = personService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;

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
                var person = this.mapper.Map<Person>(personModel);
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
                .Select(p => this.mapper.Map<GridPersonViewModel>(p));

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
