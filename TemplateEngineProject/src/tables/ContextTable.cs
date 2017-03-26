using System;
using System.Collections.Generic;

namespace TemplateEngineProject.tables
{
    class ContextTable: ICloneable
    {
        private readonly Dictionary<String, object> _properties = new Dictionary<string, object>();

        public object GetProperty(String key)
        {
            _properties.TryGetValue(key, out object property);
            if (property == null)
                throw new Exception("No such property");
            
            return property;
        }

        public void AddProperty(String key, object value) => _properties[key] = value;
        public void UpdateProperty(String key, object value) => _properties[key] = value;

        public object Clone()
        {
            ContextTable clone = new ContextTable();

            foreach(var pair in _properties)
                clone.AddProperty(pair.Key, pair.Value);

            return clone;
        }
    }
}
