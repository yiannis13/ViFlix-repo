using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Data;
using Common.Models.Domain;
using Common.Models.Dtos;

namespace ViFlix.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/rental")]
        public async Task<IHttpActionResult> RentMovies([FromBody] RentalDto rental)
        {
            var numberOfMoviesToBeRent = 0;
            var movieList = new List<string>();

            if (rental?.MovieNames == null || rental.MovieNames.Count == 0)
            {
                return BadRequest();
            }

            var customer = await _unitOfWork.Customers.GetAsync(rental.CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            foreach (var movieName in rental.MovieNames)
            {
                var movie = await _unitOfWork.Movies.GetMovieByName(movieName);
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
                _unitOfWork.Rentals.Add(rent);
            }

            if (numberOfMoviesToBeRent == 0)
                return BadRequest("No movies available to rent");

            await _unitOfWork.SaveAsync();

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
