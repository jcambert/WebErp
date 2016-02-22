using Ninject;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace WebErp.Data.Validations
{
    public interface IModelBaseValidation<T>:IInitializable where T : class
    {
        DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items);
    }
}