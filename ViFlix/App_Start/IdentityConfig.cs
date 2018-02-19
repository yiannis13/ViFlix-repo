using Common.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ViFlix.DataAccess.Configuration;

namespace ViFlix
{
    public class IdentityConfig
    {
        private readonly IConfigurationHandler _configHandler;

        public IdentityConfig()
        {
            _configHandler = new ConfigurationHandler();
        }

        public IdentityConfig(IConfigurationHandler configHandler)
        {
            _configHandler = configHandler;
        }

        public void Configuration(IAppBuilder app)
        {
            // set up Owin Context for creating Application Managers like RoleManager, AppUserManager, etc
            _configHandler.CreateAppManagers(app);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            });
        }
    }
}