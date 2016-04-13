using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Parsers.Attributes
{ 
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    internal class SectionParserAttribute : Attribute
    {
        private readonly string _name;


        public SectionParserAttribute(string sectionName)
        {
            Contract.Requires(!sectionName.IsNullOrEmpty(), "Section Name cannot be null or empty");
            this._name = sectionName;
        }

        public string SectionName => _name;
    }

}
