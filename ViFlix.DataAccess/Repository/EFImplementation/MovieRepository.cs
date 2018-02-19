using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;


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
            if (dbMovies == null)
                return new List<Movie>();

            return dbMovies.Select(Converter.ToModelMovie).ToList();
        }

        public void Remove(Movie model)
        {
            _viFlixContext.Movies.Remove(Converter.ToEntityMovie(model));
        }

        public async Task<Movie> GetMovieByName(string name)
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
    }
}