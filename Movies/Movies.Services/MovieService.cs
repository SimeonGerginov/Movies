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

        public MovieService(IRepository<Movie> movieRepository)
        {
            Guard.WhenArgument(movieRepository, "Movie Repository").IsNull().Throw();

            this.movieRepository = movieRepository;
        }

        public void AddMovie(Movie movie)
        {
            Guard.WhenArgument(movie, "Movie").IsNull().Throw();

            movie.CreatedOn = DateTime.UtcNow;
            this.movieRepository.Add(movie);
        }

        public bool DeleteMovie(string movieName)
        {
            var targetMovie = this.movieRepository.GetAllFiltered(m => m.Name == movieName).FirstOrDefault();

            if (targetMovie == null)
            {
                return false;
            }

            this.movieRepository.Delete(targetMovie);
            return true;
        }

        public void UpdateMovie(Movie movieToUpdate)
        {
            var targetMovie = this.movieRepository.GetAllFiltered(m => m.Id == movieToUpdate.Id).FirstOrDefault();

            if (targetMovie != null)
            {
                targetMovie.ModifiedOn = DateTime.UtcNow;
                this.movieRepository.Update(targetMovie);
            }
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return this.movieRepository.GetAll();
        }
    }
}