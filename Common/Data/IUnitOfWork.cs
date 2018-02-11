using System;
using System.Threading.Tasks;
using Common.Data.Repository;

namespace Common.Data
{
    /// <summary>
    /// Exposes several Repositories 
    /// </summary>
    /// <remarks>
    /// Similar to DbContext impementation in EntityFramework
    /// </remarks>
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        IMovieRepository Movies { get; }

        IMembershipTypeRepository MembershipTypes { get; }

        IRentalRepository Rentals { get; }

        Task SaveAsync();
    }
}