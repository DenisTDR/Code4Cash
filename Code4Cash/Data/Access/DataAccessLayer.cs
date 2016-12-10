using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Base;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Access
{
    public class DataAccessLayer: IDataLayer
    {
        private readonly DatabaseLayer _dbLayer;
        private readonly AccountEntity _account;

        private readonly Dictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();


        public DataAccessLayer(int accountId)
        {
            _dbLayer = new DatabaseLayer();
            _account = _dbLayer.Repo<AccountEntity>().GetOne(acc => acc.Id == accountId).Result;
        }

        public void Dispose()
        {
            _dbLayer.Dispose();
        }

        public IGenericRepository<T> Repo<T>() where T : Entity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var dbRepo = _dbLayer.Repo<T>();
                var repoInstance = new AccessRepository<T>(_account, dbRepo);
                _repositories.Add(typeof(T), repoInstance);
            }

            return (AccessRepository<T>)_repositories[typeof(T)];
        }

        public IRepository Repository(Type entityType)
        {
            throw new NotImplementedException();
        }
    }
}