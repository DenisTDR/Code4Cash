using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Misc.Attributes;

namespace Code4Cash.Misc
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropeties<T>(CustomPropertyTypes propertyTypes) where T : Entity
        {
            var entityType = typeof(T);


            //TODO: this is not open-closed
            var updatableProperties = entityType.GetProperties().Where(prop =>
            {
                if ((propertyTypes & CustomPropertyTypes.Updatable) == CustomPropertyTypes.Updatable
                    && !prop.IsCustomUpdatable())
                {
                    return false;
                }
                if ((propertyTypes & CustomPropertyTypes.Entity) == CustomPropertyTypes.Entity
                    && !prop.PropertyType.IsCustomEntity())
                {
                    return false;
                }
                if ((propertyTypes & CustomPropertyTypes.NonEntity) == CustomPropertyTypes.NonEntity
                    && prop.PropertyType.IsCustomEntity())
                {
                    return false;
                }
                if ((propertyTypes & CustomPropertyTypes.Primitive) == CustomPropertyTypes.Primitive
                    && !prop.PropertyType.IsPrimitive())
                {
                    return false;
                }

                if ((propertyTypes & CustomPropertyTypes.Enumerable) == CustomPropertyTypes.Enumerable)
                {
                    if (!typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) || prop.PropertyType.IsPrimitive())
                    {
                        return false;
                    }
                }

                return true;
            });
            return updatableProperties;
        }


        public static bool IsCustomUpdatable(this PropertyInfo propertyInfo)
        {
            var upAttrs =
                      propertyInfo.GetCustomAttributes(false)
                          .Where(attr => attr is UpdatableAttribute)
                          .Cast<UpdatableAttribute>()
                          .ToList();
            return !upAttrs.Any() || upAttrs.Any(upAttr => upAttr.Updatable);
        }
        public static bool IsCustomEntity(this Type type)
        {
            return type.IsSubclassOf(typeof(Entity));
        }
        
        public static bool IsPrimitive(this Type type)
        {
            return type.IsBooleanType() || type.IsStringType() || type.IsNumericType() ||
                   typeof(DateTime).IsAssignableFrom(type);
        }
        public static bool IsBooleanType(this Type type)
        {
            return type.IsAssignableFrom(typeof(bool));
        }
        public static bool IsStringType(this Type type)
        {
            return type.IsAssignableFrom(typeof(string));
        }
        
        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsGenericAndAsync(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException();
            }
            return methodInfo.IsGenericMethod &&
                   methodInfo.GetCustomAttributes().Any(attr => attr is AsyncStateMachineAttribute);
        }
    }
}