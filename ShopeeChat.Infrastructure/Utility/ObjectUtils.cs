using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace ShopeeChat.Infrastructure.Utility
{
    public static class ObjectUtils
    {
        public static Dictionary<string, string> ToDictionary(this object obj)
        {
            var type = obj.GetType();

            if (type.Equals(typeof(ExpandoObject)))
            {
                return (obj as ExpandoObject).ToDictionary();
            }

            var dict = new Dictionary<string, string>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                dict.Add(prop.Name.ToLower(), ConvertPropValueToString(value));
            }

            return dict;
        }

        public static Dictionary<string, string> Dyn2Dict(dynamic dynObj)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj))
            {
                string obj = propertyDescriptor.GetValue(dynObj);
                dictionary.Add(propertyDescriptor.Name, obj);
            }

            return dictionary;
        }

        public static string ConvertPropValueToString(object value)
        {
            if (value == null)
                return null;

            var valueType = value.GetType();

            if (valueType.IsPrimitive || valueType.Name.Equals("String"))
                return value.ToString();
            if (valueType.Equals(typeof(DateTime)))
                return ((DateTime)(object)value).ToString("yyyy-MM-ddTHH:mm:ssZ");
            if (valueType == typeof(List<string>))
                return string.Join(",", (List<string>)(object)valueType);
            return "";
        }
    }
}