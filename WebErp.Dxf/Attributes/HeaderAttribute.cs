using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    internal class HeaderAttribute : NameAttribute
    {
      

        public HeaderAttribute(string name):base(name)
        {
            
        }


    }

}
