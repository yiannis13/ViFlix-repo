using System.Web.Mvc;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return IndexWhenAuthenticated();

            return View("Index");
        }

        public ActionResult IndexWhenAuthenticated()
        {
            return View("IndexWhenAuthenticated");
        }

    }
}
