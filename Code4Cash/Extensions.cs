using System;

namespace Code4Cash
{
    public static class Extensions
    {
        public static bool IsSubclassOfGenericType(this Type type, Type genericType)
        {
            var tmpType = type;
            if (tmpType == genericType)
            {
                return false;
            }
            while (tmpType != null && tmpType != typeof(object))
            {
                var crt = tmpType.IsGenericType ? tmpType.GetGenericTypeDefinition() : tmpType;
                if (crt == genericType)
                {
                    return true;
                }
                tmpType = tmpType.BaseType;
            }

            return false;
        }
    }
}