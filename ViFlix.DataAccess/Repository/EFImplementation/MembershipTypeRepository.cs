using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;

namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class MembershipTypeRepository : IMembershipTypeRepository
    {
        private readonly ViFlixContext _viFlixContext;

        public MembershipTypeRepository(ViFlixContext context)
        {
            _viFlixContext = context;
        }

        public void Add(MembershipType model)
        {
            _viFlixContext.MembershipTypes.Add(Converter.ToEntityMembershipType(model));
        }

        public async Task<MembershipType> GetAsync(object id)
        {
            Entities.MembershipType membershipType = await _viFlixContext.MembershipTypes.FindAsync(id);
            if (membershipType == null)
                return new MembershipType();

            return Converter.ToModelMembershipType(membershipType);
        }

        public async Task<IList<MembershipType>> GetAllAsync()
        {
            List<Entities.MembershipType> membershipTypes = await _viFlixContext.MembershipTypes.ToListAsync();
            if (membershipTypes == null)
                return new List<MembershipType>();

            return membershipTypes.Select(Converter.ToModelMembershipType).ToList();
        }

        public void Remove(MembershipType model)
        {
            _viFlixContext.MembershipTypes.Remove(Converter.ToEntityMembershipType(model));
        }
    }
}