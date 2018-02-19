using System;
using System.Data.Entity;
using Unity;
using Common.Configuration;
using Common.Data;
using Common.Factories;
using Unity.Injection;
using Unity.Lifetime;
using ViFlix.DataAccess.Configuration;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Factories;
using ViFlix.DataAccess.Repository.EFImplementation;
using Owin;
using ViFlix.DataAccess.Repository;

namespace ViFlix
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Register your type's mappings here.
            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new PerResolveLifetimeManager(),
                new InjectionConstructor(typeof(ViFlixContext))
            );
            container.RegisterType<IConfigurationHandler, ConfigurationHandler>();
            container.RegisterType<UserManagerFactory, AppUserManagerFactory>();
            container.RegisterType<SignInManagerFactory, AppSignInManagerFactory>();
        }
    }
}