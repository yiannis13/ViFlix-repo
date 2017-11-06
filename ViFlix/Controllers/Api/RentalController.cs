using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Dtos;

namespace ViFlix.Controllers.Api
{
    public class RentalController : ApiController
    {
        private readonly ViFlixContext _context;

        public RentalController(ViFlixContext context)
        {
            _context = context;
        }

        public RentalController()
        {
            _context = new ViFlixContext();
        }

        [HttpPost]
        [Route("api/rental")]
        public async Task<IHttpActionResult> RentMovies([FromBody] RentalDto rental)
        {
            if (rental == null)
                return BadRequest();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == rental.CustomerId);
            if (customer == null)
                return NotFound();

            foreach (var movieId in rental.MovieIds)
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                if (movie == null)
                    continue;

                movie.NumberInStock--;
                var rent = new Rental
                {
                    Customer = customer,
                    DateRented = DateTime.Today,
                    Movie = movie,
                    DateReturned = DateTime.Today.AddDays(3)
                };

                _context.Rentals.Add(rent);

            }

            await _context.SaveChangesAsync();

            return StatusCode(HttpStatusCode.Created);
        }
    }
}
