using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AAA.Shared
{
    public static class Extensions
    {
        public static bool In<T>(this T input, IEnumerable<T> list)
        {
            return list.Contains(input);
        }

        public static bool GetValue(this Enum enumeration)
        {
            BooleanTypeAttribute attrib = GetAttribute<BooleanTypeAttribute>(enumeration);

            return attrib != null && attrib.BooleanValue;
        }

        private static TAttribute GetAttribute<TAttribute>(Enum enumeration) where TAttribute : Attribute
        {
            Type type = enumeration.GetType();
            MemberInfo[] info = type.GetMember(enumeration.ToString());

            if (info == null || !info.Any()) return null;

            object[] attributes = info.First().GetCustomAttributes(typeof(TAttribute), inherit: false);

            return attributes.FirstOrDefault() as TAttribute;
        }

        public static string ToWords(this string input)
        {
            return Regex.Replace(input, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>|p{Ll}))", " $0");
        }

        public static string TrimAll(this string input)
        {
            return Regex.Replace(input, @"\s+", string.Empty).Trim();
        }

        public static string ToTitleCase(this string input)
        {
            return Regex.Replace(input, @"\b\w", (match) => match.ToString().ToUpper());
        }
    }
}
