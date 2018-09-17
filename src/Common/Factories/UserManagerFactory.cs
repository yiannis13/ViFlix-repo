using System.Web;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;

namespace Common.Factories
{
    public abstract class UserManagerFactory
    {
        public abstract UserManager<AppUser> Create(HttpContextBase httpContext);
    }
}