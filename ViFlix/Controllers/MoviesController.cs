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
        public async Task<ActionResult> CreateMovie(MovieFormViewModel viewModel)
        {
            var movie = new Movie
            {
                Name = viewModel.Movie.Name,
                ReleasedDate = viewModel.Movie.ReleasedDate,
                Genre = viewModel.Genre,
                NumberInStock = viewModel.Movie.NumberInStock,
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
        public async Task<ActionResult> EditMovie(MovieFormViewModel viewModel)
        {
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