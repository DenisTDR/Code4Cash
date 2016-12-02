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
        private readonly bool _databseOwnsContext;

        public Database(Code4CashDbContext dbContext, bool databaseOwnsContext = true)
        {
            _dbContext = dbContext;
            _databseOwnsContext = databaseOwnsContext;
            _repositories = new Dictionary<Type, IRepository>();
        }

        public Database() : this(new Code4CashDbContext())
        {
        }

        public void Dispose()
        {
            if (_databseOwnsContext)
            {
                _dbContext.Dispose();
            }
            _repositories.Clear();
        }


        public Repository<T> Repository<T>() where T : Entity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repoType = typeof(Repository<T>);
                var repoInstance = (Repository<T>) Activator.CreateInstance(repoType, _dbContext);
                _repositories.Add(typeof(T), repoInstance);
            }

            return (Repository<T>) _repositories[typeof(T)];
        }

        public Repository<T> Repo<T>() where T : Entity
        {
            return this.Repository<T>();
        }
    }
}