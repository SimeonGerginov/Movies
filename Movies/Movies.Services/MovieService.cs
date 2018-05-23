using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> movieRepository;
        private readonly IRepository<Genre> genreRepository;

        public MovieService(IRepository<Movie> movieRepository, IRepository<Genre> genreRepository)
        {
            Guard.WhenArgument(movieRepository, "Movie Repository").IsNull().Throw();
            Guard.WhenArgument(genreRepository, "Genre Repository").IsNull().Throw();

            this.movieRepository = movieRepository;
            this.genreRepository = genreRepository;
        }

        public void AddMovie(Movie movie, string genreName)
        {
            Guard.WhenArgument(movie, "Movie").IsNull().Throw();

            var movieExists = this.movieRepository
                .GetAllFiltered(m => m.Name == movie.Name && m.Year == movie.Year)
                .Any();

            if (movieExists)
            {
                throw new InvalidOperationException("Movie already exists!");
            }

            var movieGenre = this.genreRepository
                .GetAllFiltered(g => g.Name == genreName)
                .FirstOrDefault();

            Guard.WhenArgument(movieGenre, "Movie Genre").IsNull().Throw();

            movie.GenreId = movieGenre.Id;
            movie.CreatedOn = DateTime.UtcNow;

            this.movieRepository.Add(movie);
        }

        public bool DeleteMovie(int movieId)
        {
            var targetMovie = this.movieRepository
                .GetAllFiltered(m => m.Id == movieId)
                .FirstOrDefault();

            if (targetMovie == null)
            {
                return false;
            }

            this.movieRepository.Delete(targetMovie);
            return true;
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            var targetMovie = this.movieRepository
                .GetAllFiltered(m => m.Id == movieToUpdate.Id)
                .FirstOrDefault();

            if (targetMovie != null)
            {
                targetMovie.Name = movieToUpdate.Name;
                targetMovie.Year = movieToUpdate.Year;
                targetMovie.RunningTime = movieToUpdate.RunningTime;
                targetMovie.Description = movieToUpdate.Description;
                targetMovie.Rating = movieToUpdate.Rating;
                targetMovie.ModifiedOn = DateTime.UtcNow;

                this.movieRepository.Update(targetMovie);
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.movieRepository.GetAllAndIncludeEntity("Genre");
        }
    }
}
