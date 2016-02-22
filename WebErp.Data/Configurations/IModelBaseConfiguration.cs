using Ninject;

namespace WebErp.Data.Configurations
{
    public interface IModelBaseConfiguration<T>:IInitializable where T : class
    {
        void ConfigureModel();
    }
}