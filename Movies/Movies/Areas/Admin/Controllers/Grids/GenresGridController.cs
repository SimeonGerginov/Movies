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

namespace Movies.Web.Areas.Admin.Controllers.Grids
{
    public class GenresGridController : AdminController
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenresGridController(IGenreService genreService, IMapper mapper)
        {
            Guard.WhenArgument(genreService, "Genre Service").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.genreService = genreService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ReadGenres([DataSourceRequest] DataSourceRequest request)
        {
            var genres = this.genreService
                .GetAllGenres()
                .Select(g => this.mapper.Map<GridGenreViewModel>(g))
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
                var genre = this.mapper.Map<Genre>(genreModel);
                this.genreService.UpdateGenre(genre);
            }

            return this.Json(new[] { genreModel });
        }
    }
}
