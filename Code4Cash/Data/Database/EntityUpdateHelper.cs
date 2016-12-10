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

        public async Task<List<PropertyInfo>> Update(T source, T destination, DataRepository<T> dataRepository)
        {
            var list = new List<PropertyInfo>();

            dataRepository.Attach(destination);


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
                    set = UpdateEntityProperties(sourceValue, out valueToSet, dataRepository.DatabaseLayer);
                    skipTriggerRepo = true;
                }


                if (set)
                {
                    propertyInfo.SetValue(destination, valueToSet);
                    list.Add(propertyInfo);

                    if (!skipTriggerRepo)
                    {
                        dataRepository.SetModifiedProperty(destination, propertyInfo.Name);
                    }
                }
            }


            await dataRepository.SaveChangesAsync();
            return list;
        }

        private bool UpdatePrimitiveProperty(object value, out object valueToSet)
        {
            valueToSet = value;
            //TODO: DateTime check is not open-closed ...
            return !(value is DateTime) || (DateTime) value != DateTime.MinValue;
        }

        private bool UpdateEntityProperties(object value, out object valueToSet, DatabaseLayer databaseLayer)
        {
            valueToSet = null;
            var valueAsEntity = (Entity) value;
            if (!Utilis.IsValidSelector(valueAsEntity.Selector))
            {
                return false;
            }
            valueToSet = databaseLayer.Repository(valueAsEntity.GetType()).GetOneEntityBySelector(valueAsEntity.Selector).Result;
            
            return true;
        }
    }
}