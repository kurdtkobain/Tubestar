using System;
using System.Linq;

namespace TubeStar
{
    public static class EnumHelpers
    {
        public static T GetAttribute<T>(this Enum enumeration)
            where T : Attribute
        {
            return enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
        }

        public static bool HasAttribute<T>(this Enum enumeration)
            where T : Attribute
        {
            return GetAttribute<T>(enumeration) != null;
        }
    }
}