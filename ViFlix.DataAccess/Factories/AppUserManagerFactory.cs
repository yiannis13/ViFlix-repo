using System.Web;
using Common.Factories;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ViFlix.DataAccess.Factories
{
    public class AppUserManagerFactory : UserManagerFactory
    {
        public override UserManager<AppUser> Create(HttpContextBase httpContext)
        {
            return httpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
        }
    }
}
