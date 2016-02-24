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
    
    public abstract class ModelBaseConfiguration<T> : IModelBaseConfiguration<T> where T : class
    {
        protected  EntityTypeConfiguration<T> Builder;

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
            if (typeof(IModelBase).IsAssignableFrom(typeof(T)))
            {
                var _builder = (EntityTypeConfiguration<IModelBase>)Convert.ChangeType(Builder, typeof(EntityTypeConfiguration<IModelBase>));
                _builder.HasKey(e => new { e.Societe, e.Code });
            }
        }
    }
}
