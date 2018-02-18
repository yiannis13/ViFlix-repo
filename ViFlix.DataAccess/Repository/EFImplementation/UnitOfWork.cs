using System;
using System.Threading.Tasks;
using Common.Data;
using Common.Data.Repository;
using ViFlix.DataAccess.DbContextContainer;

namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ViFlixContext _context;

        public UnitOfWork(ViFlixContext context)
        {
            _context = context ?? throw new NullReferenceException("DbContext cannot be null");

            Customers = new CustomerRepository(_context);
            Movies = new MovieRepository(_context);
            MembershipTypes = new MembershipTypeRepository(_context);
            Rentals = new RentalRepository(_context);
        }

        public ICustomerRepository Customers { get; }

        public IMovieRepository Movies { get; }

        public IMembershipTypeRepository MembershipTypes { get; }

        public IRentalRepository Rentals { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}