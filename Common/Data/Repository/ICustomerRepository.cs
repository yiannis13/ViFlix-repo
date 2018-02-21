using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models.Domain;

namespace Common.Data.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IList<Customer>> GetCustomersWithMembershipTypeAsync();

        Task<Customer> GetCustomerWithMembershipTypeAsync(int id);

        Task<Customer> ModifyCustomerAsync(Customer customer);
    }
}