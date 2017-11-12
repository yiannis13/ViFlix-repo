using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViFlix.DataAccess.Identity;
using ViFlix.ViewModels;
using WebGrease.Css.Extensions;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {

        [Route("signup")]
        public ActionResult SignUpForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUpUser(AppUserViewModel user)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var signInManager = new SignInManager<AppUser, string>(userManager, authenticationManager);

            if (ModelState.IsValid)
            {
                if (user.Password != user.ConfirmedPassword)
                    ModelState.AddModelError("", @"password does not match");

                var appUser = new AppUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser, isPersistent: true, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }

                result.Errors.ForEach(error => ModelState.AddModelError("", error));
            }

            return View(user);
        }
    }
}