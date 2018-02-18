using Owin;

namespace Common.Configuration
{
    public interface IConfigurationHandler
    {
        void CreateAppManagers(IAppBuilder app);
    }
}