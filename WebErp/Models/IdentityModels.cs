using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebErp.Data;
using WebErp.Data.Infrastructure;
using Ninject;
using WebErp.Data.Configurations;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using WebErp.Data.Validations;
using WebErp.Data.Models;
using System;
using WebErp.Data.Repositories;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;
using WebErp.Initializers;
using Ninject.Modules;
using Ninject.Extensions.Conventions.Syntax;
using System.Data.Entity.Migrations;

namespace WebErp.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser, consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : User
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Notez que authenticationType doit correspondre à l'instance définie dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Ajouter des revendications d’utilisateur personnalisées ici
            return userIdentity;
        }
    }


    public class ApplicationDbContext : WebErpContext<ApplicationUser>
    {
        public ApplicationDbContext():base()
        {
            //DbMigrationsConfiguration.AutomaticMigrationsEnabled = True;
        }
        [Inject]
        public ApplicationDbContext(IDbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var modeltype = entityEntry.Entity.GetType();
            var validationType = typeof(IModelBaseValidation<>).MakeGenericType(modeltype);
            var validations = Kernel.GetAll(validationType).ToList();
            foreach (var validation in validations)
            {
                var v = validation as IModelBaseValidation;
                var result = v.ValidateEntity(entityEntry, items);
                if (result.ValidationErrors.Any())
                    return result;
            }
            return base.ValidateEntity(entityEntry, items);

                
        }
        public override void Initialize()
        {
            base.Initialize();
            // var validations = Kernel.GetAll(typeof(IModelBaseValidation<ApplicationUser>)).ToList();

            /*var modeltype = typeof(ApplicationUser);
            var validationType = typeof(IModelBaseValidation<>).MakeGenericType(modeltype);
            var validations = Kernel.GetAll(validationType).ToList();*/
            
            var initializer = Kernel.TryGet <System.Data.Entity.IDatabaseInitializer<ApplicationDbContext>> ();
            if (initializer != null)
            {
                //initializer.InitializeDatabase(this);
                Database.CreateIfNotExists();
                Database.SetInitializer(initializer);
            }
        }


        public override IKernel Kernel
        {
            get
            {
                if(base.Kernel==null)
                {
                    return new StandardKernel(new ApplicationModules());
                }
                return base.Kernel;
            }

            set
            {
                base.Kernel = value;
            }
        }

    }
    
    public class ApplicationModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind(typeof(IModelBaseRepository<>)).To(typeof(ModelBaseRepository<>));
            Bind<IDbContextOptions>().To<DbContextOptions>().InSingletonScope();
            Bind(typeof(IDbSet<>)).To(typeof(IocDbSet<>)).When(_ => Kernel.Get<IDbContextOptions>().InMemory == false).InRequestScope();
            Bind(typeof(IDbSet<>)).To(typeof(FakeDbSet<>)).When(_ => Kernel.Get<IDbContextOptions>().InMemory == true).InRequestScope();
            Bind<IContext>().To<ApplicationDbContext>().InRequestScope();
            Bind(typeof(IDatabaseInitializer<ApplicationDbContext>)).To(typeof(ApplicationDbInitializer)).When(_ => Kernel.Get<IDbContextOptions>().RecreateDatabase);

            Bind<IModelConfiguration>(Kernel, typeof(Article));
            Bind<IModelBaseValidation>(Kernel, typeof(Article));
            //kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(IModelBaseConfiguration)).BindAllInterfaces());
            //kernel.Bind(x => x.FromThisAssembly().SelectAllClasses().InheritedFrom(typeof(IModelBaseValidation)).BindAllInterfaces());

            var tmp = Kernel.GetAll<IModelConfiguration>().ToList();
        }
        private  void Bind<T>(IKernel kernel, params Type[] types)
        {
            var _types = types.ToList();
            _types.Insert(0, typeof(ApplicationUser));
            _types.ForEach(t =>
            {
                Action<IFromSyntax> a = x => x.FromAssemblyContaining(t).SelectAllClasses().InheritedFrom<T>().BindAllInterfaces();
                kernel.Bind(a);
            });

        }
    }
}