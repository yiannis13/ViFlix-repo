using System.Collections.Generic;
using System.Threading.Tasks;
using ViFlix.DataAccess.Models;

namespace ViFlix.Repository.Contract
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IList<Customer>> GetCustomersWithMembershipTypeAsync();
        Task<Customer> GetCustomerWithMembershipTypeAsync(int id);
    }
}