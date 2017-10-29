using System.Web.Mvc;
using ViFlix.Models;

namespace ViFlix.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Random()
        {
            var movie = new Movie
            {
                Id = 1,
                Name = "Seven"
            };

            return View(movie);
        }
    }
}