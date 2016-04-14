using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;

namespace WebErp.Dxf
{
    public static class FieldSectionProvider
    {
        static FieldSectionProvider()
        {
            _fieldsCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        }
        private readonly static Dictionary<Type, Dictionary<string, PropertyInfo>> _fieldsCache;
        public static Dictionary<string,PropertyInfo> GetFields<TTYPE,T>() where T :NameAttribute
        {
            if (_fieldsCache.ContainsKey(typeof(TTYPE)))
                return _fieldsCache[typeof(TTYPE)];
            Dictionary<string, PropertyInfo> _fields = new Dictionary<string, PropertyInfo>();
            Type header = typeof(TTYPE);// Document.Header.GetType();
            foreach (PropertyInfo info in header.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (info.CanWrite && info.CanRead)
                {
                    object[] attrs = info.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        T casted = attr as T;
                        if (casted != null)
                        {
                            _fields[casted.Name] = info;
                        }
                    }
                }
            }
            _fieldsCache[typeof(TTYPE)] = _fields;
            return _fields;
        }
    }
}
