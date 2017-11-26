using System.Collections.Generic;
using System.Data.Entity;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Repository.Contract;
using System.Threading.Tasks;

namespace ViFlix.Repository.EFImplementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ViFlixContext context)
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

        public async Task<IList<Customer>> GetCustomersWithMembershipTypeAsync()
        {
            return await ViFlixContext.Customers.Include(c => c.MembershipType).ToListAsync();
        }

        public async Task<Customer> GetCustomerWithMembershipTypeAsync(int id)
        {
            return await ViFlixContext.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}