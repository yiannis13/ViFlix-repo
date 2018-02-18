using Common.Configuration;
using Common.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Identity;

namespace ViFlix.DataAccess.Configuration
{
    public class ConfigurationHandler : IConfigurationHandler
    {
        public void CreateAppManagers(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ViFlixContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<RoleManager<AppRole>>(
                (options, context) => new RoleManager<AppRole>(
                    new RoleStore<AppRole>(context.Get<ViFlixContext>())));
        }
    }
}