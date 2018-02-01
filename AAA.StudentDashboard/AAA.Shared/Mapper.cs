using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AAA.Shared
{
    public static class Mapper
    {
        public static void MapProperties<T>(string field, T obj, PropertyInfo property) where T : new()
        {
            if (property == null) return;

            if (property.PropertyType.IsEnum)
            {
                Type enumType = property.PropertyType;

                string fieldValue = field.TrimAll();
                property.SetValue(obj, Enum.Parse(enumType, fieldValue), null);
            }
            else
            {
                property.SetValue(obj, field, null);
            }
        }
    }
}
