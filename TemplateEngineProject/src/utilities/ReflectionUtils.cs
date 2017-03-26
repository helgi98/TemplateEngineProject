using System;
using System.Reflection;
using TemplateEngineProject.tables;
using System.Linq;

namespace TemplateEngineProject.utilities
{
    static class ReflectionUtils
    {
        public static Object GetProperty(object obj, string propertyName)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties())
                if (property.Name == propertyName)
                    return property.GetValue(obj);

            throw new Exception($"No such property [{propertyName}]");
        }

        public static Object GetObjectProperty(object obj, params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
                obj = GetProperty(obj, Char.ToUpperInvariant(propertyName[0]) + propertyName.Substring(1));

            return obj;
        }

        public static Object GetObjectFromContext(ContextTable context, string objectPath)
        {
            string[] objData = objectPath.Split('.');

            string objName = objData[0];

            object obj = context.GetProperty(objName);

            return GetObjectProperty(obj, objData.Skip(1).ToArray());
        }

    }
}