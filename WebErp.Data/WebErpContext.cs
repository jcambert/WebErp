
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

            new ArticleConfiguration(modelBuilder);
            /*new UserConfiguration(modelBuilder);
            new RoleConfiguration(modelBuilder);
            new UserRoleConfiguration(modelBuilder);*/
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }

        public void Initialize()
        {
            IDbSets= this.GetType().GetProperties().Where(p => p.GetMethod.ReturnType == typeof(IDbSet<>)).ToList();
        }

        private List<PropertyInfo> IDbSets { get; set; }

        /*public new IDbSet<T> Set<T>()where T : class
        {
           
        }*/
        #region Entity Sets
        [Inject]
        public IDbSet<Article> ArticleSet { get; set; }
        //public IDbSet<User> UserSet { get; set; }
        //public IDbSet<Role> RoleSet { get; set; }
        //public IDbSet<UserRole> UserRoleSet { get; set; }
        #endregion
    }
}
