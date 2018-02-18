using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Factories;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ViFlix.ViewModels;
using WebGrease.Css.Extensions;

namespace ViFlix.Controllers
{
    [AllowAnonymous]
    public class SignUpController : Controller
    {
        private readonly UserManagerFactory _userManagerFactory;
        private readonly SignInManagerFactory _signInManagerFactory;

        public SignUpController(UserManagerFactory userManagerFactory, SignInManagerFactory signInManagerFactory)
        {
            _userManagerFactory = userManagerFactory ?? throw new NullReferenceException("userManagerFactory cannot be null");
            _signInManagerFactory = signInManagerFactory ?? throw new NullReferenceException("signInManagerFactory cannot be null");
        }

        [Route("signup")]
        public ActionResult SignUpForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUpUser(SignUpViewModel user)
        {
            UserManager<AppUser> userManager = _userManagerFactory.Create(HttpContext);
            SignInManager<AppUser, string> signInManager = _signInManagerFactory.Create(HttpContext, userManager);

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
                    //await roleManager.CreateAsync(new AppRole(RoleName.Admin));
                    //await userManager.AddToRoleAsync(appUser.Id, RoleName.Admin);

                    await signInManager.SignInAsync(appUser, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("IndexWhenAuthenticated", "Home");
                }

                result.Errors.ForEach(error => ModelState.AddModelError("", error));
            }

            return View(user);
        }

    }
}