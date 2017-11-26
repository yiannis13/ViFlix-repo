using System.Data.Entity;
using System.Threading.Tasks;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Repository.Contract;

namespace ViFlix.Repository.EFImplementation
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ViFlixContext context)
            : base(context)
        {
        }

        public ViFlixContext ViFlixContext
        {
            get
            {
                return Context as ViFlixContext;
            }
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            return await ViFlixContext.Movies.SingleOrDefaultAsync(m => m.Name == name);
        }
    }
}