using System;
using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            Guard.WhenArgument(genreRepository, "Genre Repository").IsNull().Throw();

            this.genreRepository = genreRepository;
        }

        public void Add(Genre genre)
        {
            Guard.WhenArgument(genre, "Genre").IsNull().Throw();

            genre.CreatedOn = DateTime.UtcNow;
            this.genreRepository.Add(genre);
        }
    }
}