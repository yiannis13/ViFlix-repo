using System.Web;
using Common.Factories;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ViFlix.DataAccess.Factories
{
    public class AppSignInManagerFactory : SignInManagerFactory
    {
        public override SignInManager<AppUser, string> Create(HttpContextBase httpContext, UserManager<AppUser> userManager)
        {
            IAuthenticationManager authenticationManager = httpContext.GetOwinContext().Authentication;
            return new SignInManager<AppUser, string>(userManager, authenticationManager);
        }
    }
}