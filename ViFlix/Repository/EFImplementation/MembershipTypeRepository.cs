using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Repository.Contract;

namespace ViFlix.Repository.EFImplementation
{
    public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(ViFlixContext context)
            : base(context)
        {
        }
    }
}