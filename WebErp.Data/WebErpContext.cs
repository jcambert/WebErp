
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebErp.Data.Configurations;
using WebErp.Data.Infrastructure;
using WebErp.Data.Models;
using WebErp.Models;

namespace WebErp.Data
{
    public interface IContext:IDisposable
    {
        IDbSet<Article> ArticleSet { get; set; }

        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        int SaveChanges();
        void Commit();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
       
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task CommitAsync();
        bool RequireUniqueEmail { get; set; }
    }

   
    public class WebErpContext<TUser> : IdentityDbContext<TUser>,IContext,IInitializable  where TUser:User
    {
        protected readonly IDbContextOptions _dbContextOptions;
        public WebErpContext()
        {
          
        }
        public WebErpContext(IDbContextOptions options) : base(options.NameOrConnectionString)
        {
            this._dbContextOptions = options;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var configs = Kernel.GetAll(typeof(IModelConfiguration)).ToList();
            configs.ForEach(c => ((IModelConfiguration)c).ConfigureModel(modelBuilder));

            new ArticleConfiguration().ConfigureModel(modelBuilder);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }

        public virtual void Initialize()
        {
            
        }

        IDbSet<TEntity> IContext.Set<TEntity>()
        {
            return base.Set<TEntity>() as IDbSet<TEntity>;
        }

        
        [Inject]
        public virtual IKernel Kernel
        {
            get;
            set;
        }

        #region Entity Sets
        [Inject]
        public virtual IDbSet<Article> ArticleSet { get; set; }

        [Inject]
        public override IDbSet<IdentityRole> Roles { get; set; }

        [Inject]
        public override IDbSet<TUser> Users { get; set; }
        #endregion
    }
}
