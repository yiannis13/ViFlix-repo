﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Dtos;

namespace ViFlix.Controllers.Api
{
    public class RentalController : ApiController
    {

        [HttpPost]
        [Route("api/rental")]
        public async Task<IHttpActionResult> RentMovies([FromBody] RentalDto rental)
        {
            var numberOfMoviesToBeRent = 0;

            if (rental?.MovieIds == null || rental.MovieIds.Count == 0)
                return BadRequest();

            using (var context = new ViFlixContext())
            {
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == rental.CustomerId);
                if (customer == null)
                    return NotFound();

                foreach (var movieId in rental.MovieIds)
                {
                    var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
                    if (movie == null || movie.NumberAvailable < 1)
                        continue;

                    numberOfMoviesToBeRent++;
                    movie.NumberAvailable--;
                    var rent = new Rental
                    {
                        Customer = customer,
                        DateRented = DateTime.Now,
                        Movie = movie,
                        DateToBeReturned = movie.ReleasedDate != null && CalculateAge(movie.ReleasedDate.Value) < 1
                            ? DateTime.Today.AddDays(1)
                            : DateTime.Today.AddDays(3)
                    };
                    context.Rentals.Add(rent);
                }

                if (numberOfMoviesToBeRent == 0)
                    return BadRequest("No movies available to rent");

                await context.SaveChangesAsync();
            }

            return Ok();
        }

        private static int CalculateAge(DateTime releasedDate)
        {
            var now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var dateOfBirth = int.Parse(releasedDate.ToString("yyyyMMdd"));

            return (now - dateOfBirth) / 10000;
        }

    }
}
