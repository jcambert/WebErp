using Ninject.Modules;
using WebErp.Data;
using WebErp.Data.Infrastructure;
using WebErp.Data.Repositories;
using WebErp.Services;

namespace WebErp.Ioc
{


    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind(typeof(IModelBaseRepository<>)).To(typeof(ModelBaseRepository<>));
            Bind<WebErpContext>().ToSelf().WithConstructorArgument("options", DatabaseOptions.Options);

            Bind<IEncryptionService>().To<EncryptionService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}