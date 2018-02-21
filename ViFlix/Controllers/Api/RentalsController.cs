using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Data;
using Common.Models.Domain;
using Common.Models.Dto;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Repository;

namespace ViFlix.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalsController() :
            this(new UnitOfWork(new ViFlixContext()))
        {
        }

        public RentalsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("api/rental")]
        public async Task<IHttpActionResult> RentMovies([FromBody] RentalDto rental)
        {
            // Todo: Refactor by introducing a Service for it

            var numberOfMoviesToBeRent = 0;
            var movieList = new List<string>();

            if (rental?.MovieNames == null || rental.MovieNames.Count == 0)
            {
                return BadRequest();
            }

            Customer customer = await _unitOfWork.Customers.GetAsync(rental.CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            foreach (var movieName in rental.MovieNames)
            {
                Movie movie = await _unitOfWork.Movies.GetMovieByNameAsync(movieName);
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

            await _unitOfWork.CompleteAsync();

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
