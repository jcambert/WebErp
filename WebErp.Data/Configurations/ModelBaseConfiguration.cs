
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
    /// <summary>
    /// Model Base Configuration
    /// </summary>
    /// <typeparam name="T">Must be IModelBase</typeparam>
    public abstract class ModelBaseConfiguration<T> where T : class, IModelBase
    {
        protected readonly EntityTypeConfiguration<T> Builder;

        // private readonly ModelBuilder Builder;
        public ModelBaseConfiguration(DbModelBuilder builder) 
        {
            this.Builder = builder.Entity<T>();
            Initialize();
        }

        /// <summary>
        /// Initialize Model Configuration
        /// </summary>
        protected void Initialize()
        {
            Builder.HasKey(e => new { e.Societe, e.Code });
            ConfigureModel();
        }

        /// <summary>
        /// Configure the model
        /// set Require or Length, etc...
        /// </summary>
        protected abstract void ConfigureModel();
    }
}
