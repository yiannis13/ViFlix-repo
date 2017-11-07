using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.ViewModels;

namespace ViFlix.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ViFlixContext _context;

        public MoviesController(ViFlixContext context)
        {
            _context = context;
        }

        public MoviesController()
        {
            _context = new ViFlixContext();
        }

        [Route("movies")]
        [HttpGet]
        public async Task<ActionResult> GetMovies()
        {
            IList<Movie> movies = await _context.Movies.ToListAsync();

            if (!movies.Any())
                return HttpNotFound();

            return View(movies);
        }

        [HttpGet]
        [Route("movies/Details/{id}")]
        public async Task<ActionResult> GetMovie(int id)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [HttpGet]
        [Route("movies/new")]
        public ActionResult CreateMovieForm()
        {
            return View(new MovieFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMovie(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                    Movie = new Models.Movie()
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
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return RedirectToAction("GetMovies");
        }

        [HttpGet]
        [Route("movies/edit/{id}")]
        public async Task<ActionResult> EditMovieForm(int id)
        {
            var dBmovie = await _context.Movies.SingleAsync(m => m.Id == id);
            if (dBmovie == null)
                return HttpNotFound();

            var movie = new MovieFormViewModel
            {
                Movie = new Models.Movie
                {
                    Id = dBmovie.Id,
                    Name = dBmovie.Name,
                    ReleasedDate = dBmovie.ReleasedDate,
                    NumberInStock = dBmovie.NumberInStock
                },
                Genre = dBmovie.Genre
            };

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMovie(MovieFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                    Movie = new Models.Movie()
                    {
                        Name = viewModel.Movie.Name,
                        NumberInStock = viewModel.Movie.NumberInStock
                    },
                    Genre = viewModel.Genre
                };

                return View("EditMovieForm", model);
            }

            var updatedMovie = await _context.Movies.SingleAsync(m => m.Id == viewModel.Movie.Id);

            updatedMovie.Name = viewModel.Movie.Name;
            updatedMovie.ReleasedDate = viewModel.Movie.ReleasedDate;
            updatedMovie.NumberInStock = viewModel.Movie.NumberInStock;
            updatedMovie.Genre = viewModel.Genre;

            await _context.SaveChangesAsync();

            return RedirectToAction("GetMovies");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}