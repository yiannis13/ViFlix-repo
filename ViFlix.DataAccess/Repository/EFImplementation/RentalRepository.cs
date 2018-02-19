using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;


namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ViFlixContext _viFlixContext;

        public RentalRepository(ViFlixContext context)
        {
            _viFlixContext = context;
        }

        public void Add(Rental model)
        {
            _viFlixContext.Rentals.Add(Converter.ToEntityRental(model));
        }

        public async Task<Rental> GetAsync(object id)
        {
            Entities.Rental dbRental = await _viFlixContext.Rentals.FindAsync(id);
            if (dbRental == null)
                return new Rental();

            return Converter.ToModelRental(dbRental);
        }

        public async Task<IList<Rental>> GetAllAsync()
        {
            List<Entities.Rental> dbRentals = await _viFlixContext.Rentals.ToListAsync();
            if (dbRentals == null)
                return new List<Rental>();

            return dbRentals.Select(Converter.ToModelRental).ToList();
        }

        public void Remove(Rental model)
        {
            _viFlixContext.Rentals.Remove(Converter.ToEntityRental(model));
        }
    }
}