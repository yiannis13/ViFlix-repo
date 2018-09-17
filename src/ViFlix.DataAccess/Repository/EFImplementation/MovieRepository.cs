using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Common.Data.Repository;
using Common.Models.Domain;
using Common.Models.ViewModels;
using ViFlix.DataAccess.DbContextContainer;
using Genre = ViFlix.DataAccess.Entities.Genre;


namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ViFlixContext _viFlixContext;

        public MovieRepository(ViFlixContext context)
        {
            _viFlixContext = context;
        }

        public void Add(Movie model)
        {
            _viFlixContext.Movies.Add(Converter.ToEntityMovie(model));
        }

        public async Task<Movie> GetAsync(object id)
        {
            Entities.Movie dbMovie = await _viFlixContext.Movies.FindAsync(id);
            if (dbMovie == null)
                return new Movie();

            return Converter.ToModelMovie(dbMovie);
        }

        public async Task<IList<Movie>> GetAllAsync()
        {
            List<Entities.Movie> dbMovies = await _viFlixContext.Movies.ToListAsync();

            return dbMovies.Select(Converter.ToModelMovie).ToList();
        }

        public async void Remove(Movie model)
        {
            Entities.Movie dbMovie = await _viFlixContext.Movies.FindAsync(model.Id);
            if (dbMovie == null)
                return;

            _viFlixContext.Movies.Remove(dbMovie);
        }

        public async Task<Movie> GetMovieByNameAsync(string name)
        {
            Entities.Movie dbMovie = await _viFlixContext.Movies.SingleOrDefaultAsync(m => m.Name == name);

            if (dbMovie == null)
                return new Movie();

            return new Movie
            {
                Id = dbMovie.Id,
                Name = dbMovie.Name,
                NumberAvailable = dbMovie.NumberAvailable,
                NumberInStock = dbMovie.NumberInStock,
                ReleasedDate = dbMovie.ReleasedDate
            };
        }

        public async Task<Movie> ModifyMovieWithGenreAsync(MovieFormViewModel movieWithGenre)
        {
            Entities.Movie movieToBeUpdated = await _viFlixContext.Movies.FindAsync(movieWithGenre.Movie.Id);
            if (movieToBeUpdated == null)
                return null;

            movieToBeUpdated.Name = movieWithGenre.Movie.Name;
            movieToBeUpdated.ReleasedDate = movieWithGenre.Movie.ReleasedDate;
            movieToBeUpdated.NumberInStock = movieWithGenre.Movie.NumberInStock;
            movieToBeUpdated.Genre = (Genre)movieWithGenre.Genre;

            // add the missing Genre value
            movieWithGenre.Movie.Genre = movieWithGenre.Genre;

            return movieWithGenre.Movie;
        }
    }
}