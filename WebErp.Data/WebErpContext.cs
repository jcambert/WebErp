
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Data.Configurations;
using WebErp.Data.Infrastructure;
using WebErp.Models;

namespace WebErp.Data
{
    public class WebErpContext : DbContext,IInitializable
    {
        protected readonly IDbContextOptions _dbContextOptions;
        public WebErpContext(IDbContextOptions options) : base(options.NameOrConnectionString)
        {
            this._dbContextOptions = options;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
        

    
        #region Entity Sets
        [Inject]
        public virtual IDbSet<Article> ArticleSet { get; set; }
        #endregion
    }
}
