using System.Threading.Tasks;
using Common.Models.Domain;
using Common.Models.ViewModels;

namespace Common.Data.Repository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetMovieByNameAsync(string name);

        Task<Movie> ModifyMovieWithGenreAsync(MovieFormViewModel movie);
    }
}