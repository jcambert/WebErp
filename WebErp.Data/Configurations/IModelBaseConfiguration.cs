using Ninject;
using System.Data.Entity;

namespace WebErp.Data.Configurations
{
    public interface IModelBaseConfiguration
    {
        void ConfigureModel(DbModelBuilder builder);
    }
    public interface IModelBaseConfiguration<T>:IModelBaseConfiguration where T : class
    {
    }
}