using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Data
{
    public class IocDbSet<T>:DbSet<T> where T :class
    {
        public IocDbSet()
        {

        }
    }
}
