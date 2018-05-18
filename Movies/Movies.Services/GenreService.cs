using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddGenre(Genre genre)
        {
            Guard.WhenArgument(genre, "Genre").IsNull().Throw();

            genre.CreatedOn = DateTime.UtcNow;
            this.genreRepository.Add(genre);
        }

        public bool DeleteGenre(string genreName)
        {
            var targetGenre = this.genreRepository.GetAllFiltered(g => g.Name == genreName).FirstOrDefault();

            if (targetGenre == null)
            {
                return false;
            }

            this.genreRepository.Delete(targetGenre);
            return true;
        }

        public void UpdateGenre(Genre genreToUpdate)
        {
            var targetGenre = this.genreRepository.GetAllFiltered(g => g.Id == genreToUpdate.Id).FirstOrDefault();

            if (targetGenre != null)
            {
                targetGenre.ModifiedOn = DateTime.UtcNow;
                this.genreRepository.Update(targetGenre);
            }
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return this.genreRepository.GetAll();
        }
    }
}
