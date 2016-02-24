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

namespace WebErp.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser, consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Notez que authenticationType doit correspondre à l'instance définie dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Ajouter des revendications d’utilisateur personnalisées ici
            return userIdentity;
        }
    }


    public class ApplicationDbContext : WebErpContext
    {
        [Inject]
        public ApplicationDbContext(IDbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var configs = Kernel.GetAll(typeof(IModelBaseConfiguration)).ToList();
            configs.ForEach(c => ((IModelBaseConfiguration)c).ConfigureModel(modelBuilder));

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
                System.Data.Entity.Database.SetInitializer(initializer);
        }

        [Inject]
        public IKernel Kernel { get; set; }
    }
}