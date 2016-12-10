using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Misc;

namespace Code4Cash.Data.Database
{
    public class EntityUpdateHelper<T> where T : Entity
    {

        public async Task<List<PropertyInfo>> Update(T source, T destination, Repository<T> repository)
        {
            var list = new List<PropertyInfo>();

            repository.Attach(destination);


            var props =
                ReflectionExtensions.GetPropeties<T>(CustomPropertyTypes.Updatable);
            foreach (var propertyInfo in props)
            {
                var sourceValue = propertyInfo.GetValue(source);
                if (sourceValue == null) continue;
                object valueToSet = null;
                var set = false;
                var skipTriggerRepo = false;
                if (propertyInfo.PropertyType.IsPrimitive())
                {
                    set = UpdatePrimitiveProperty(sourceValue, out valueToSet);
                }
                else if (propertyInfo.PropertyType.IsCustomEntity())
                {
                    set = UpdateEntityProperties(sourceValue, out valueToSet, repository.DatabaseUnit);
                    skipTriggerRepo = true;
                }


                if (set)
                {
                    propertyInfo.SetValue(destination, valueToSet);
                    list.Add(propertyInfo);

                    if (!skipTriggerRepo)
                    {
                        repository.SetModifiedProperty(destination, propertyInfo.Name);
                    }
                }
            }


            await repository.SaveChangesAsync();
            return list;
        }

        private bool UpdatePrimitiveProperty(object value, out object valueToSet)
        {
            valueToSet = value;
            //TODO: DateTime check is not open-closed ...
            return !(value is DateTime) || (DateTime) value != DateTime.MinValue;
        }

        private bool UpdateEntityProperties(object value, out object valueToSet, DatabaseUnit databaseUnit)
        {
            valueToSet = null;
            var valueAsEntity = (Entity) value;
            if (!Utilis.IsValidSelector(valueAsEntity.Selector))
            {
                return false;
            }
            valueToSet = databaseUnit.Repository(valueAsEntity.GetType()).GetOneEntityBySelector(valueAsEntity.Selector).Result;
            
            return true;
        }
    }
}