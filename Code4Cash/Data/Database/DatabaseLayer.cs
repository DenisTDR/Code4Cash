using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Base;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Database
{
    public class DatabaseLayer : IDataLayer
    {
        private readonly Code4CashDbContext _dbContext;
        private readonly bool _databseOwnsContext;

        private readonly Dictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();

        public DatabaseLayer(Code4CashDbContext dbContext, bool databaseOwnsContext = true)
        {
            _dbContext = dbContext;
            _databseOwnsContext = databaseOwnsContext;
        }

        public DatabaseLayer() : this(new Code4CashDbContext())
        {
        }

        public void Dispose()
        {
            if (_databseOwnsContext)
            {
                _dbContext.Dispose();
            }
            _repositories?.Clear();
        }


        public IGenericRepository<T> Repository<T>() where T : Entity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repoInstance = new DataRepository<T>(_dbContext, this);
                _repositories.Add(typeof(T), repoInstance);
            }

            return (DataRepository<T>)_repositories[typeof(T)];
        }

        public IGenericRepository<T> Repo<T>() where T : Entity
        {
            return Repository<T>();
        }

        public IRepository Repository(Type entityType)
        {
            if (!entityType.IsSubclassOf(typeof(Entity)))
            {
                throw new Exception("Only 'Entity' subclasses are stored In database.");
            }
            if (!_repositories.ContainsKey(entityType))
            {
                var repoType = typeof(DataRepository<>);
                var repoGenericType = repoType.MakeGenericType(entityType);
                var repo = (IRepository)Activator.CreateInstance(repoGenericType, _dbContext, this);
                _repositories.Add(entityType, repo);
            }
            return _repositories[entityType];
        }
    }
}