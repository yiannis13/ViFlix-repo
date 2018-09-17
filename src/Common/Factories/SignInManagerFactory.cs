using System.Web;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Common.Factories
{
    public abstract class SignInManagerFactory
    {
        public abstract SignInManager<AppUser, string> Create(HttpContextBase httpContext, UserManager<AppUser> userManager);
    }
}