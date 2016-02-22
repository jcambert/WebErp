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
        public virtual void Initialize()
        {
            
        }

        public abstract DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items);
    }
}
