using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;

namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
    {
        public MembershipTypeRepository(ViFlixContext context)
            : base(context)
        {
        }
    }
}