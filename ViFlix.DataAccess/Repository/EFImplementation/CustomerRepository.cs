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

            return dbCustomers.Select(Converter.ToModelCustomer).ToList();
        }

        public async void Remove(Customer customer)
        {
            Entities.Customer dbCustomer = await _viFlixContext.Customers.FindAsync(customer.Id);
            if (dbCustomer == null)
                return;

            _viFlixContext.Customers.Remove(dbCustomer);
        }

        public async Task<IList<Customer>> GetCustomersWithMembershipTypeAsync()
        {
            List<Entities.Customer> dBCustomers = await _viFlixContext.Customers.Include(c => c.MembershipType).ToListAsync();

            return dBCustomers.Select(Converter.ToModelCustomer).ToList();
        }

        public async Task<Customer> GetCustomerWithMembershipTypeAsync(int id)
        {
            Entities.Customer dbCustomer = await _viFlixContext.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return new Customer();

            return Converter.ToModelCustomer(dbCustomer);
        }

        public async Task<Customer> ModifyCustomerAsync(Customer customer)
        {
            Entities.Customer oldCustomer = await _viFlixContext.Customers.FindAsync(customer.Id);
            if (oldCustomer == null)
                return null;

            oldCustomer.Name = customer.Name;
            oldCustomer.Birthday = customer.Birthday;
            oldCustomer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            oldCustomer.MembershipTypeId = customer.MembershipTypeId;

            return customer;
        }
    }
}