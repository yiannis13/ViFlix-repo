using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Entities;
using Rental = Common.Models.Domain.Rental;


namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ViFlixContext _viFlixContext;

        public RentalRepository(ViFlixContext context)
        {
            _viFlixContext = context;
        }

        public async void Add(Rental model)
        {
            Customer customer = await _viFlixContext.Customers.FindAsync(model.Customer.Id);
            Movie movie = await _viFlixContext.Movies.FindAsync(model.Movie.Id);

            var rentalToBeSaved = new Entities.Rental
            {
                Customer = customer,
                Movie = movie,
                DateRented = model.DateRented,
                DateToBeReturned = model.DateToBeReturned
            };

            _viFlixContext.Rentals.Add(rentalToBeSaved);
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

            return dbRentals.Select(Converter.ToModelRental).ToList();
        }

        public async void Remove(Rental model)
        {
            Entities.Rental dbRental = await _viFlixContext.Rentals.FindAsync(model.Id);
            if (dbRental == null)
                return;

            _viFlixContext.Rentals.Remove(dbRental);
        }
    }
}