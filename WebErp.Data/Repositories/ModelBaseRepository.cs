
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebErp.Models;

namespace WebErp.Data.Repositories
{
   
    public class ModelBaseRepository<T> : IModelBaseRepository<T>,IInitializable
            where T : class, IModelBase, new()
    {
        
        public void Initialize()
        {
            this.Context = Kernel.Get<IContext>();
        }

        #region Properties
        [Inject]
        public IKernel Kernel { get; set; }

        

        protected IContext Context
        {
            get; private set;
        }
        public ModelBaseRepository()
        {
        }
        #endregion
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public virtual IQueryable<T> All
        {
            get
            {
                return GetAll();
            }
        }
        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        public T GetSingle(string id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            
            var dbEntityEntry = Context.Entry<T>(entity);
            Context.Set<T>().Add(entity);
        }
        public virtual void Edit(T entity)
        {
            var dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            var dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Dispose()
        {
            
        }

        public T ElementAt(int index)
        {
            return All.Skip(index).First();
        }
    }
}
