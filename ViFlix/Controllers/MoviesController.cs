using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Data;
using Common.Models;
using Common.Models.Domain;
using ViFlix.ViewModels;

namespace ViFlix.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("movies")]
        public async Task<ActionResult> GetMovies()
        {
            IList<Movie> movies = await _unitOfWork.Movies.GetAllAsync();
            if (!movies.Any())
                return HttpNotFound();

            if (User.IsInRole(RoleName.Admin))
                return View("GetMovies", movies);

            return View("GetMoviesReadOnly", movies);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        [Route("movies/new")]
        public ActionResult CreateMovieForm()
        {
            return View(new MovieFormViewModel());
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMovie(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                    Movie = new Movie()
                    {
                        Name = viewModel.Movie.Name,
                        NumberInStock = viewModel.Movie.NumberInStock
                    },
                    Genre = viewModel.Genre
                };

                return View("CreateMovieForm", model);
            }

            var movie = new Movie
            {
                Name = viewModel.Movie.Name,
                ReleasedDate = viewModel.Movie.ReleasedDate,
                Genre = viewModel.Genre,
                NumberInStock = viewModel.Movie.NumberInStock,
                NumberAvailable = viewModel.Movie.NumberInStock,
                DateAdded = DateTime.Now
            };
            _unitOfWork.Movies.Add(movie);

            await _unitOfWork.SaveAsync();

            return RedirectToAction("GetMovies");
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        [Route("movies/edit/{id}")]
        public async Task<ActionResult> EditMovieForm(int id)
        {
            Movie movie = await _unitOfWork.Movies.GetAsync(id);
            if (movie == null)
                return HttpNotFound();

            var viewModelMovie = new MovieFormViewModel
            {
                Movie = new Movie
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    ReleasedDate = movie.ReleasedDate,
                    NumberInStock = movie.NumberInStock
                },
                Genre = movie.Genre
            };

            return View(viewModelMovie);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMovie(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                    Movie = new Movie()
                    {
                        Name = viewModel.Movie.Name,
                        NumberInStock = viewModel.Movie.NumberInStock
                    },
                    Genre = viewModel.Genre
                };

                return View("EditMovieForm", model);
            }

            var updatedMovie = await _unitOfWork.Movies.GetAsync(viewModel.Movie.Id);

            updatedMovie.Name = viewModel.Movie.Name;
            updatedMovie.ReleasedDate = viewModel.Movie.ReleasedDate;
            updatedMovie.NumberInStock = viewModel.Movie.NumberInStock;
            updatedMovie.Genre = viewModel.Genre;

            await _unitOfWork.SaveAsync();

            return RedirectToAction("GetMovies");
        }
    }
}