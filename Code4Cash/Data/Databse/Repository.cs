using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Databse
{
    public class Repository<TE>: IRepository where TE: Entity
    {
        private readonly Code4CashDbContext _dbContext;
        public Repository(Code4CashDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TE>> All()
        {
            var dbSet = _dbContext.Set<TE>();
            var all = await dbSet.ToListAsync();

            return all;
        }

        public async Task<TE> GetOne(string selector)
        {
            var dbSet = _dbContext.Set<TE>();
            var obj =
                await
                    dbSet.FirstOrDefaultAsync(
                        e => e.Selector.Equals(selector, StringComparison.InvariantCultureIgnoreCase));
            return obj;
        }

        public async Task<TE> Update(string selector, TE entity)
        {
            var existing = await this.GetOne(selector);
            return null;
        }

        public async Task<TE> Add(TE entity)
        {
            var dbSet = _dbContext.Set<TE>();
            entity = dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
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
    }
}