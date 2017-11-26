using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Dtos;
using ViFlix.Repository.EFImplementation;

namespace ViFlix.Controllers.Api
{
    public class RentalsController : ApiController
    {

        [HttpPost]
        [Route("api/rental")]
        public async Task<IHttpActionResult> RentMovies([FromBody] RentalDto rental)
        {
            var numberOfMoviesToBeRent = 0;
            var movieList = new List<string>();

            if (rental?.MovieNames == null || rental.MovieNames.Count == 0)
                return BadRequest();

            using (var unitOfWork = new UnitOfWork(new ViFlixContext()))
            {
                var customer = await unitOfWork.Customers.GetAsync(rental.CustomerId);
                if (customer == null)
                    return NotFound();

                foreach (var movieName in rental.MovieNames)
                {
                    var movie = await unitOfWork.Movies.GetMovieByName(movieName);
                    if (movie == null || movie.NumberAvailable < 1)
                        continue;

                    numberOfMoviesToBeRent++;
                    movie.NumberAvailable--;
                    var rent = new Rental
                    {
                        Customer = customer,
                        DateRented = DateTime.Now,
                        Movie = movie,
                        // if the movie is older than 1 year, it becomes a 3-day rented movie.
                        DateToBeReturned = movie.ReleasedDate != null && CalculateReleaseDate(movie.ReleasedDate.Value) < 1
                            ? DateTime.Today.AddDays(1)
                            : DateTime.Today.AddDays(3)
                    };
                    movieList.Add(movie.Name);
                    unitOfWork.Rentals.Add(rent);
                }

                if (numberOfMoviesToBeRent == 0)
                    return BadRequest("No movies available to rent");

                await unitOfWork.SaveAsync();
            }

            return Ok(movieList);
        }

        private static int CalculateReleaseDate(DateTime releasedDate)
        {
            var now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var release = int.Parse(releasedDate.ToString("yyyyMMdd"));

            return (now - release) / 10000;
        }

    }
}
