using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebErp.Dxf.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class TableAttribute : Attribute
    {
        private readonly string _tableName;
        private readonly Type _tableParser;

        public string TableName => _tableName;
        public Type TableParser => _tableParser;

        public TableAttribute(string name, Type parser)
        {
            this._tableName = name;
            this._tableParser = parser;
        }
    }

}
