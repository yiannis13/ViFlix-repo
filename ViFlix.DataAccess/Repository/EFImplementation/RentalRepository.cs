using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;


namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(ViFlixContext context)
            : base(context)
        {
        }
    }
}