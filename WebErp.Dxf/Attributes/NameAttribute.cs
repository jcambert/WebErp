using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Attributes
{
    public abstract class NameAttribute:Attribute
    {
        private readonly string _name;
        public NameAttribute(string name)
        {
            Contract.Requires(name!=null, "Attribute Name cannot be null or empty");
            this._name = name;
        }

        public string Name => _name;
    }
}
