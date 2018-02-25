using System.Web.Http;
using System.Web.Mvc;
using Common.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Unity.AspNet.Mvc;
using ViFlix.DataAccess.Configuration;

namespace ViFlix
{
    public class Startup
    {
        private readonly IConfigurationHandler _configHandler;

        public Startup()
        : this(new ConfigurationHandler())
        {
        }

        public Startup(IConfigurationHandler configHandler)
        {
            _configHandler = configHandler;
        }

        /// <summary>
        /// It is called by OWIN 
        /// </summary>
        /// <param name="app">The application builder which is injected by OWIN</param>
        public void Configuration(IAppBuilder app)
        {
            // Dependency resolver for WebAPI controllers 
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(UnityConfig.Container);

            // Dependency resolver for MVC controllers 
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

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