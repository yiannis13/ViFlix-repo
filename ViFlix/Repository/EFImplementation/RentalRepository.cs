using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Repository.Contract;

namespace ViFlix.Repository.EFImplementation
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(ViFlixContext context)
            : base(context)
        {
        }
    }
}