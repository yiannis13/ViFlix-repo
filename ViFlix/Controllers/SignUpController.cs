using System;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ViFlix.DataAccess.DbContextContainer;
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
        public async Task<ActionResult> SignUpUser(SignUpViewModel user)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var signInManager = new SignInManager<AppUser, string>(userManager, authenticationManager);

            //if (user.Password != user.ConfirmedPassword)
            //    ModelState.AddModelError("", Properties.Resources.PasswordDoesNotMatch);

            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    // **Uncomment that code when you want to generate an new role to a user. Then, run the Application and SignUp.**
                    //var roleStore = new RoleStore<AppRole>(new ViFlixContext());
                    //RoleManager<AppRole> roleManager = new RoleManager<AppRole>(roleStore);
                    //await roleManager.CreateAsync(new AppRole("admin"));
                    //await userManager.AddToRoleAsync(appUser.Id, "admin");

                    await signInManager.SignInAsync(appUser, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("IndexWhenAuthenticated", "Home");
                }

                result.Errors.ForEach(error => ModelState.AddModelError("", error));
            }

            return View(user);
        }

    }
}