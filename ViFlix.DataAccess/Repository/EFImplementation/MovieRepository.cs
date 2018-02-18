using System.Data.Entity;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;


namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ViFlixContext context)
            : base(context)
        {
        }

        public ViFlixContext ViFlixContext => Context as ViFlixContext;

        public async Task<Movie> GetMovieByName(string name)
        {
            Entities.Movie dbMovie = await ViFlixContext.Movies.SingleOrDefaultAsync(m => m.Name == name);

            if (dbMovie == null)
            {
                return new Movie();
            }

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