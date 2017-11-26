using System.Threading.Tasks;
using ViFlix.DataAccess.Models;

namespace ViFlix.Repository.Contract
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetMovieByName(string name);
    }
}