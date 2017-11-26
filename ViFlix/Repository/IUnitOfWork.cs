using System;
using System.Threading.Tasks;
using ViFlix.Repository.Contract;

namespace ViFlix.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IMovieRepository Movies { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        IRentalRepository Rentals { get; }

        Task SaveAsync();
    }
}