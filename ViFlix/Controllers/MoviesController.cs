using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViFlix.Models;

namespace ViFlix.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IList<Movie> _movies = new List<Movie>
        {
            new Movie {Id = 1, Name = "Titanic"},
            new Movie {Id = 2, Name = "catch me if you can"}
        };

        [Route("movies")]
        [HttpGet]
        public ActionResult GetMovies()
        {
            if (_movies == null)
                return HttpNotFound();

            return View(_movies);
        }

        [HttpGet]
        [Route("movies/Details/{id}")]
        public ActionResult GetMovie(int id)
        {
            Movie movie = _movies.FirstOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

    }
}