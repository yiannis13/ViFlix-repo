using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;

namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ViFlixContext _viFlixContext;

        public CustomerRepository(ViFlixContext context)
        {
            _viFlixContext = context;
        }

        public void Add(Customer customer)
        {
            _viFlixContext.Customers.Add(Converter.ToEntityCustomer(customer));
        }

        public async Task<Customer> GetAsync(object id)
        {
            Entities.Customer dbCustomer = await _viFlixContext.Customers.FindAsync(id);
            if (dbCustomer == null)
                return new Customer();

            return Converter.ToModelCustomer(dbCustomer);
        }

        public async Task<IList<Customer>> GetAllAsync()
        {
            List<Entities.Customer> dbCustomers = await _viFlixContext.Customers.ToListAsync();
            if (dbCustomers == null)
                return new List<Customer>();

            return dbCustomers.Select(Converter.ToModelCustomer).ToList();
        }

        public void Remove(Customer customer)
        {
            _viFlixContext.Customers.Remove(Converter.ToEntityCustomer(customer));
        }

        public async Task<IList<Customer>> GetCustomersWithMembershipTypeAsync()
        {
            List<Entities.Customer> dBCustomers = await _viFlixContext.Customers.Include(c => c.MembershipType).ToListAsync();
            if (dBCustomers == null)
                return new List<Customer>();

            return dBCustomers.Select(Converter.ToModelCustomer).ToList();
        }

        public async Task<Customer> GetCustomerWithMembershipTypeAsync(int id)
        {
            Entities.Customer dbCustomer = await _viFlixContext.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return new Customer();

            return Converter.ToModelCustomer(dbCustomer);
        }
    }
}