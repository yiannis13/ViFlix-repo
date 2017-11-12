using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using ViFlix.DataAccess.Identity;
using ViFlix.ViewModels;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [Route("login")]
        public ActionResult LoginForm()
        {
            return View();
        }

        public async Task<ActionResult> LoginUser(LoginViewModel user)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var signInManager = new SignInManager<AppUser, string>(userManager, authenticationManager);

            if (ModelState.IsValid)
            {
                var appUser = await userManager.FindAsync(user.Email, user.Password);
                if (appUser != null)
                {
                    await signInManager.SignInAsync(appUser, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", @"Invalid username or password");

            return View(user);
        }

        public ActionResult LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}