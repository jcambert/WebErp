using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Metadata.Internal;
using System;
using System.Collections.Generic;
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
        protected readonly EntityTypeBuilder<T> Builder;

        // private readonly ModelBuilder Builder;
        public ModelBaseConfiguration(ModelBuilder builder) 
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
