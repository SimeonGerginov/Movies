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
    public class GenresGridController : AdminController
    {
        private readonly IGenreService genreService;

        public GenresGridController(IGenreService genreService)
        {
            Guard.WhenArgument(genreService, "Genre Service").IsNull().Throw();

            this.genreService = genreService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadGenres([DataSourceRequest] DataSourceRequest request)
        {
            var genres = this.genreService
                .GetAllGenres()
                .Map<Genre, GridGenreViewModel>()
                .ToDataSourceResult(request);

            return this.Json(genres);
        }

        [SaveChanges]
        public ActionResult DeleteGenre(GridGenreViewModel genreModel)
        {
            if (genreModel != null)
            {
                this.genreService.DeleteGenre(genreModel.Name);
            }

            return this.Json(new[] { genreModel });
        }

        [SaveChanges]
        public ActionResult EditGenre(GridGenreViewModel genreModel)
        {
            if (genreModel != null)
            {
                var genre = MappingService.MappingProvider.Map<Genre>(genreModel);
                this.genreService.UpdateGenre(genre);
            }

            return this.Json(new[] { genreModel });
        }
    }
}
