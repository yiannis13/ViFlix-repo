using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Factories;
using Common.Models.Identity;
using Common.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManagerFactory _userManagerFactory;
        private readonly SignInManagerFactory _signInManagerFactory;

        public LoginController(UserManagerFactory userManagerFactory, SignInManagerFactory signInManagerFactory)
        {
            _userManagerFactory = userManagerFactory ?? throw new NullReferenceException("userManagerFactory cannot be null");
            _signInManagerFactory = signInManagerFactory ?? throw new NullReferenceException("signInManagerFactory cannot be null");
        }

        [Route("login")]
        public ActionResult LoginForm()
        {
            return View();
        }

        public async Task<ActionResult> LoginUser(LoginViewModel user)
        {
            UserManager<AppUser> userManager = _userManagerFactory.Create(HttpContext);
            if (userManager == null)
                return View(user);

            SignInManager<AppUser, string> signInManager = _signInManagerFactory.Create(HttpContext, userManager);
            if (signInManager == null)
                return View(user);

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