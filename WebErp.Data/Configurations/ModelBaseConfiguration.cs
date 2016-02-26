using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public abstract class ModelConfiguration<T> : IModelConfiguration<T> where T : class
    {
        protected EntityTypeConfiguration<T> Builder;
        public ModelConfiguration()
        {

        }

        public virtual void ConfigureModel(DbModelBuilder builder)
        {
            this.Builder = builder.Entity<T>();
        }
    }
    public abstract class ModelBaseConfiguration<T> : IModelBaseConfiguration<T> where T : class, IModelBase
    {
        protected EntityTypeConfiguration<T> Builder;

        // private readonly ModelBuilder Builder;
        public ModelBaseConfiguration()
        {

        }



        /// <summary>
        /// Configure the model
        /// set Require or Length, etc...
        /// </summary>
        public virtual void ConfigureModel(DbModelBuilder builder)
        {
            this.Builder = builder.Entity<T>();
            Builder.HasKey(e => new { e.Societe, e.Code });

        }
    }
}
