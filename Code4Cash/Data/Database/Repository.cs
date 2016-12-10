using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Misc;
using Code4Cash.Misc.Attributes;
using Code4Cash.Misc.Exceptions;

namespace Code4Cash.Data.Database
{
    public class Repository<TE>: IRepository where TE: Entity
    {
        private readonly Code4CashDbContext _dbContext;
        public  DatabaseUnit DatabaseUnit { get; }
        public Repository(Code4CashDbContext dbContext, DatabaseUnit databaseUnit)
        {
            this._dbContext = dbContext;
            this.DatabaseUnit = databaseUnit;
        }

        public async Task<IEnumerable<TE>> All()
        {
            var dbSet = this._dbContext.Set<TE>();
            var all = await dbSet.ToListAsync();

            return all;
        }

        public async Task<TE> GetOneBySelector(string selector)
        {
            var dbSet = _dbContext.Set<TE>();
            var obj =
                await
                    dbSet.FirstOrDefaultAsync(
                        e => e.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase));
            return obj;
        }

        public async Task<TE> GetOne(Func<TE, bool> condition)
        {
            var dbSet = _dbContext.Set<TE>();
            return (await dbSet.ToListAsync()).FirstOrDefault(condition);
        }

        public async Task<Entity> GetOneEntityBySelector(string selector)
        {
            return await this.GetOneBySelector(selector);
        }

        public async Task<TE> Add(TE entity)
        {
            var dbSet = _dbContext.Set<TE>();
            entity = dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TE>> Add(params TE[] entities)
        {
            var list = new List<TE>();
            foreach (var entity in entities)
            {
                list.Add(await Add(entity));
            }
            return list;
        } 

        public async Task<int> CountAsync(Func<TE, bool> condition)
        {
            var dbSet = _dbContext.Set<TE>();
            return await dbSet.CountAsync(e => condition(e));
        }

        public int Count(Func<TE, bool> condition = null)
        {
            if (condition == null)
            {
                condition = e => true;
            }
            var dbSet = _dbContext.Set<TE>();
            return dbSet.Count(condition);
        }

        public async Task<TE> Update(string selector, TE entity)
        {
            var existing = await this.GetOneBySelector(selector);
            if (existing == null)
            {
                throw new NotFoundException();
            }
            var updatedProps = await new EntityUpdateHelper<TE>().Update(entity, existing, this);

            return existing;
        }

        public async Task<Entity> GetOneByType(string selector, Type type)
        {
            if (!type.IsSubclassOf(typeof(Entity)))
            {
                throw new Exception("Only 'Entity' types are stored In database.");
            }
            if (type == typeof(TE))
            {
                return await this.GetOneBySelector(selector);
            }

            var typeRepo = this.DatabaseUnit.Repository(type);
            
            var entity = await typeRepo.GetOneEntityBySelector(selector);
            
            return entity;
        }
        public void Attach(TE entity)
        { 
            this._dbContext.Set<TE>().Attach(entity);
        }

        public void SetModifiedProperty(TE entity, string propertyName)
        {
            var manager = ((IObjectContextAdapter)this._dbContext).ObjectContext.ObjectStateManager;
            manager.GetObjectStateEntry(entity).SetModifiedProperty(propertyName);
        }
        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
        }
    }
}