using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Data.Infrastructure
{
    public class UnitOfWork:IUnitOfWork,IInitializable
    {
        public UnitOfWork()
        {

        }

        public IContext Context { get; private set; }

        [Inject]
        public IKernel Kernel { get; set; }

        public void Commit()
        {
            Context.Commit();
        }

        public async Task CommitAsync()
        {
            await Context.CommitAsync();
        }

        public void Initialize()
        {
            this.Context = Kernel.Get<IContext>();
        }
    }
}
