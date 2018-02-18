using Common.Data;
using Unity;
//using ViFlix.DataAccess.DbContextContainer;
//using ViFlix.DataAccess.Repository.EFImplementation;

namespace ViFlix
{
    public class CompositionRoot
    {
        public static void Compose()
        {
            IUnityContainer container = new UnityContainer();

            //container.RegisterType<IUnitOfWork, UnitOfWork>();
            //container.RegisterType(typeof(ViFlixContext));
        }
    }
}