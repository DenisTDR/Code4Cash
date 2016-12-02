using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Databse
{
    public class Database : IDisposable
    {
        private readonly Code4CashDbContext _dbContext;
        private readonly Dictionary<Type, IRepository> _repositories;

        public Database()
        {
            _dbContext = new Code4CashDbContext();
            _repositories = new Dictionary<Type, IRepository>();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            _repositories.Clear();
        }


        public Repository<T> Repository<T>() where T : Entity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repoType = typeof(Repository<T>);
                var repoInstance = (Repository<T>) Activator.CreateInstance(repoType, _dbContext);
                _repositories.Add(typeof(T),repoInstance);
            }

            return (Repository<T>)_repositories[typeof(T)];
        }

        public Repository<T> Repo<T>() where T : Entity
        {
            return this.Repository<T>();
        }
    }
}
