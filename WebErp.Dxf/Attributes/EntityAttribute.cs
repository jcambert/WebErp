using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class EntityAttribute:NameAttribute
    {
       
        public EntityAttribute(string name):base(name)
        {
          
        }

    }
}
