using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebErp.Dxf.Attributes;
using WebErp.Dxf.Entities;

namespace WebErp.Dxf
{
    public static class EntitySerializerProvider
    {
        private static readonly  Dictionary<Type, EntitySerializer> serializers;
        private static readonly List<Type> types;
        static EntitySerializerProvider()
        {
            serializers = new Dictionary<Type, EntitySerializer>();
            types = Assembly.GetAssembly(typeof(DxfDocument)).GetTypesWithAttribute<EntityAttribute>().ToList();
            foreach (var type in types)
            {
                serializers[type] = null;
            }
        }

        public static EntitySerializer Get(Type t)
        {
            if (!serializers.ContainsKey(t)) return null;
            if (serializers[t] == null)
                serializers[t] = new EntitySerializer(t);
            return serializers[t];
        }
    }

    public class EntitySerializer
    {
        string name;
        Type t;
        bool isInitialized = false;
        class _Property
        {
            public PropertyInfo Property;
            public EntityCodeAttribute Attribute;
        }
        readonly Dictionary<int, _Property> properties;
        public EntitySerializer(Type t)
        {
            properties = new Dictionary<int, _Property>();
            this.t = t;
        }

        private void Initialize()
        {
            if (isInitialized) return;
            name = t.GetCustomAttribute<EntityAttribute>()?.Name;

            var props = t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(EntityCodeAttribute)));
            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes<EntityCodeAttribute>();
                foreach (var attr in attrs)
                {
                    properties[attr.Code] = new _Property() { Property = prop, Attribute = attr };
                }
            }
            isInitialized = true;
        }

        public void Read(int groupcode, string value)
        {
            Initialize();
        }

        public string Write(object entity)
        {
            Initialize();
            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("{0}\n{1}", DxfDocument.START_GROUP_CODE, name ?? "UNDEFINED"));
            foreach (var prop in properties)
            {
                string key = prop.Key.ToString();
                string value;
                if (prop.Value.Attribute.PropertyName.IsNullOrEmpty())
                {

                    //value = prop.Value.Property.GetValue(entity)?.ToString();
                    value = GetPropertyValue(entity, prop.Value.Property.Name)?.ToString();

                }
                else
                {
                    string pname = string.Format("{0}.{1}", prop.Value.Property.Name, prop.Value.Attribute.PropertyName);
                    value = GetPropertyValue(entity, pname)?.ToString();
                }
                if (value != null)
                    sb.Append(string.Format("\n{0}\n{1}", key, value));
            }

            return sb.ToString();
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            object targetObject = obj;
            string targetPropertyName = propertyName;

            if (propertyName.Contains('.'))
            {
                string[] split = propertyName.Split('.');
                targetObject = obj.GetType().GetProperty(split[0]).GetValue(obj, null);
                targetPropertyName = split[1];
            }
            var prop = targetObject.GetType().GetProperty(targetPropertyName);
            var value= prop.GetValue(targetObject, null);
            if (prop.PropertyType == typeof(bool))
                return Boolean.Parse(value.ToString()) ? 1 : 0;
            return value;
        }
    }
}
