using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Data.Configurations;
using WebErp.Models;

namespace WebErp.Data
{
    public class WebErpContext : DbContext
    {
        public WebErpContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new ArticleConfiguration(modelBuilder);
            new UserConfiguration(modelBuilder);
            new RoleConfiguration(modelBuilder);
            new UserRoleConfiguration(modelBuilder);
        }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }

        #region Entity Sets
        public DbSet<Article> ArticleSet { get; set; }
        public DbSet<User> UserSet { get; set; }
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<UserRole> UserRoleSet { get; set; }
        #endregion
    }
}
