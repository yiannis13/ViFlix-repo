using System.Threading.Tasks;
using Common.Models.Domain;

namespace Common.Data.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetMovieByName(string name);
    }
}