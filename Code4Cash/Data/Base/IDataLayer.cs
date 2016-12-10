using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Base
{
    public interface IDataLayer:IDisposable
    {

        IGenericRepository<T> Repo<T>() where T : Entity;
        IRepository Repository(Type entityType);
    }
}