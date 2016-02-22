[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebErp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebErp.App_Start.NinjectWebCommon), "Stop")]

namespace WebErp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data.Infrastructure;
    using Data.Repositories;
    using Commmon;
    using System.Data.Entity;
    using Data;

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
            kernel.Bind<IDbContextOptions>().ToProvider<DbContextOptionsProvider>().InSingletonScope();
            kernel.Bind(typeof(IDbSet<>)).To(typeof(DbSet<>)).When(_ => kernel.Get<IDbContextOptions>().InMemory == false);
            kernel.Bind(typeof(IDbSet<>)).To(typeof(FakeDbSet<>)).When(_ => kernel.Get<IDbContextOptions>().InMemory == true);
            kernel.Bind<WebErpContext>().ToSelf().InRequestScope();
        }        
    }
}
