using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Base;
using Newtonsoft.Json;

namespace Code4Cash.Data.Database
{
    public class UpdatesLogger
    {
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        public async Task StoreUpdates(BookingEntity oldE,
            BookingEntity newE,
            List<PropertyInfo> props, 
            DatabaseLayer dbLayer)
        {
            if (props.Count == 0)
            {
                return;
            }
            var repo = dbLayer.Repo<BookingPropertyUpdateLogEntity>();
            var updateId = Rand.Next(1000, 10*1000);
            foreach (var propertyInfo in props)
            {
                var updateLog = new BookingPropertyUpdateLogEntity
                {
                    PropertyName = propertyInfo.Name,
                    OldValue = JsonConvert.SerializeObject(propertyInfo.GetValue(oldE)),
                    NewValue = JsonConvert.SerializeObject(propertyInfo.GetValue(newE)),
                    UpdateId = updateId
                };
                await repo.Add(updateLog);
            }
        }
    }
}