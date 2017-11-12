using System.Web.Mvc;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
