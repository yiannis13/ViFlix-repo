using System.Web.Mvc;

namespace ViFlix.Controllers
{
    public class RentalsController : Controller
    {
        [Route("rentals/new")]
        public ActionResult RentalForm()
        {
            return View();
        }
    }
}