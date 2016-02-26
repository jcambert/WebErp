using Ninject;
using System.Data.Entity;
using WebErp.Models;

namespace WebErp.Data.Configurations
{
    public interface IModelConfiguration
    {
        void ConfigureModel(DbModelBuilder builder);
    }
    public interface IModelBaseConfiguration:IModelConfiguration
    {
        
    }

    public interface IModelConfiguration<T> : IModelConfiguration where T :class
    {

    }
    public interface IModelBaseConfiguration<T>:IModelBaseConfiguration where T : class, IModelBase
    {
    }
}