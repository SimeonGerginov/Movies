using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using AutoMapper;
using Bytes2you.Validation;

using Movies.Common;
using Movies.Core.Models;
using Movies.Core.Models.Enums;
using Movies.Infrastructure.Attributes;
using Movies.Services.Contracts;
using Movies.Web.Areas.Admin.Controllers.Abstraction;
using Movies.Web.Areas.Admin.Controllers.Grids;
using Movies.Web.ViewModels.Admin;

namespace Movies.Web.Areas.Admin.Controllers
{
    public class PanelController : AdminController
    {
        private readonly IGenreService genreService;
        private readonly IMovieService movieService;
        private readonly IPersonService personService;
        private readonly IFileConverter fileConverter;
        private readonly IMapper mapper;

        public PanelController(
            IGenreService genreService, 
            IMovieService movieService,
            IPersonService personService, 
            IFileConverter fileConverter, 
            IMapper mapper)
        {
            Guard.WhenArgument(genreService, "Genre Service").IsNull().Throw();
            Guard.WhenArgument(movieService, "Movie Service").IsNull().Throw();
            Guard.WhenArgument(personService, "Person Service").IsNull().Throw();
            Guard.WhenArgument(fileConverter, "File Converter").IsNull().Throw();
            Guard.WhenArgument(mapper, "Mapper").IsNull().Throw();

            this.genreService = genreService;
            this.movieService = movieService;
            this.personService = personService;
            this.fileConverter = fileConverter;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddGenre()
        {
            return this.PartialView(PartialViews.AddGenre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddGenre(AddGenreViewModel genreViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction<PanelController>(c => c.Index());
            }
            
            var mappedGenre = this.mapper.Map<Genre>(genreViewModel);
            this.genreService.AddGenre(mappedGenre);

            return this.RedirectToAction<GenresGridController>(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddMovie()
        {
            var genresSelectList = this.genreService
                .GetAllGenres()
                .Select(g => new SelectListItem() { Text = g.Name, Value = g.Name });

            var movieViewModel = new AddMovieViewModel()
            {
                GenresSelectList = genresSelectList
            };

            return this.PartialView(PartialViews.AddMovie, movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddMovie([Bind(Exclude = "Image")]AddMovieViewModel movieViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction<PanelController>(c => c.Index());
            }

            if (this.Request.Files.Count > 0)
            {
                var image = this.Request.Files["Image"];
                var imageData = this.fileConverter.PostedToByteArray(image);

                movieViewModel.Image = imageData;
            }
            
            var movieModel = this.mapper.Map<Movie>(movieViewModel);
            this.movieService.AddMovie(movieModel, movieViewModel.GenreName);

            return this.RedirectToAction<MoviesGridController>(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddPerson()
        {
            return this.PartialView(PartialViews.AddPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddPerson([Bind(Exclude = "Picture")]PersonViewModel personViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.PartialView(PartialViews.AddPerson, personViewModel);
            }

            if (this.Request.Files.Count > 0)
            {
                var picture = this.Request.Files["Picture"];
                var imageData = this.fileConverter.PostedToByteArray(picture);

                personViewModel.Picture = imageData;
            }
            
            var personModel = this.mapper.Map<Person>(personViewModel);
            this.personService.AddPerson(personModel);

            return this.RedirectToAction<PeopleGridController>(c => c.Index());
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult AddPersonToMovie()
        {
            var peopleSelectList = this.personService
                .GetAllPeople()
                .Select(p => new SelectListItem()
                {
                  Text = $"{p.FirstName} {p.LastName} ({p.DateOfBirth.Year})",
                  Value = p.Id.ToString()
                });

            var moviesSelectList = this.movieService
                .GetAllMovies()
                .Select(m => new SelectListItem() { Text = $"{m.Name} ({m.Year})", Value = m.Id.ToString() });

            var perosonInMovieViewModel = new PersonInMovieViewModel()
            {
                People = peopleSelectList,
                Movies = moviesSelectList
            };

            return this.PartialView(PartialViews.AddPersonToMovie, perosonInMovieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SaveChanges]
        public ActionResult AddPersonToMovie(PersonInMovieViewModel personInMovieViewModel)
        {
            int movieId = personInMovieViewModel.MovieId;
            int personId = personInMovieViewModel.PersonId;
            Role role = personInMovieViewModel.Role;

            this.movieService.AddPersonToMovie(movieId, personId, role);

            return this.RedirectToAction<PanelController>(c => c.Index());
        }
    }
}
