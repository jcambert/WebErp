using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Data.Validations
{
    
    public abstract class ModelBaseValidation<T> : IModelBaseValidation<T> where T : class
    {
        

        public virtual DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
        }
    }
}
