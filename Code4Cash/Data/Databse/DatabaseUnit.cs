using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Databse
{
    public class DatabaseUnit : IDisposable
    {
        private readonly Code4CashDbContext _dbContext;
        private readonly bool _databseOwnsContext;

        private readonly Dictionary<Type, IRepository> _repositories = new Dictionary<Type, IRepository>();

        public DatabaseUnit(Code4CashDbContext dbContext, bool databaseOwnsContext = true)
        {
            this._dbContext = dbContext;
            this._databseOwnsContext = databaseOwnsContext;
        }

        public DatabaseUnit() : this(new Code4CashDbContext())
        {
        }

        public void Dispose()
        {
            if (this._databseOwnsContext)
            {
                this._dbContext.Dispose();
            }
            this._repositories?.Clear();
        }


        public Repository<T> Repository<T>() where T : Entity
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repoInstance = new Repository<T>(this._dbContext, this);
                this._repositories.Add(typeof(T), repoInstance);
            }

            return (Repository<T>) this._repositories[typeof(T)];
        }

        public Repository<T> Repo<T>() where T : Entity
        {
            return this.Repository<T>();
        }

        public IRepository Repository(Type entityType)
        {
            if (!entityType.IsSubclassOf(typeof(Entity)))
            {
                throw new Exception("Only 'Entity' subclasses are stored In database.");
            }
            if (!this._repositories.ContainsKey(entityType))
            {
                var repoType = typeof(Repository<>);
                var repoGenericType = repoType.MakeGenericType(entityType);
                var repo = (IRepository)Activator.CreateInstance(repoGenericType, this._dbContext, this);
                this._repositories.Add(entityType, repo);
            }
            return this._repositories[entityType];
        }
    }
}