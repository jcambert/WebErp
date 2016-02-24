[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebErp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebErp.App_Start.NinjectWebCommon), "Stop")]

namespace WebErp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Data.Infrastructure;
    using Data.Repositories;
    using System.Data.Entity;
    using Data;
    using Models;
    using Data.Configurations;
    using Configurations;
    using System.Linq;
    using Data.Validations;
    using Initializers;
    using Ninject.Extensions.Conventions.Syntax;
    using System.Collections.Generic;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);

        }

        public static Bootstrapper Bootstrapper => bootstrapper;

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
           kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind(typeof(IModelBaseRepository<>)).To(typeof(ModelBaseRepository<>));
            kernel.Bind<IDbContextOptions>().To<DbContextOptions>().InSingletonScope();
            kernel.Bind(typeof(IDbSet<>)).To(typeof(IocDbSet<>)).When(_ => kernel.Get<IDbContextOptions>().InMemory == false).InRequestScope();
            kernel.Bind(typeof(IDbSet<>)).To(typeof(FakeDbSet<>)).When(_ => kernel.Get<IDbContextOptions>().InMemory == true).InRequestScope();
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            //kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(IModelBaseConfiguration)).BindAllInterfaces());
            //kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(IModelBaseValidation)).BindAllInterfaces());
            kernel.Bind(typeof(IDatabaseInitializer<ApplicationDbContext>)).To<ApplicationDbInitializer>().When(_ => kernel.Get<IDbContextOptions>().RecreateDatabase);


            Bind<IModelBaseConfiguration>(kernel, typeof(Article));
            Bind<IModelBaseValidation>(kernel, typeof(Article));
        }
        private static void Bind<T>(IKernel kernel, params Type []types)
        {
            var ts = types.ToList();
            ts.Insert(0, typeof(ApplicationUser));
            Action<IFromSyntax> a= x=> { x.FromAssemblyContaining(ts).SelectAllClasses().InheritedFrom<T>().BindAllInterfaces(); };
            kernel.Bind(a);
        }
    }
}
