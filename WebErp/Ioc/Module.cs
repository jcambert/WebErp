using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebErp.Data;
using WebErp.Data.Infrastructure;
using WebErp.Data.Repositories;

namespace AngularJSWebApiEmpty.Ioc
{
    public class Module : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind(typeof(IModelBaseRepository<>)).To(typeof(ModelBaseRepository<>));
            Bind<WebErpContext>().ToSelf().WithConstructorArgument("options", DatabaseOptions);
        }
    }
}