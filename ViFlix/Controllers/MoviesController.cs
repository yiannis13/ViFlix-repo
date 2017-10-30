using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;

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
        public ActionResult GetMovies()
        {
            IList<Movie> movies = _context.Movies.ToList();

            if (!movies.Any())
                return HttpNotFound();

            return View(movies);
        }

        [HttpGet]
        [Route("movies/Details/{id}")]
        public ActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}