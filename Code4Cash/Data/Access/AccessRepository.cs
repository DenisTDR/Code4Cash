using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Code4Cash.Data.Base;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Access
{
    public class AccessRepository<TE>:IGenericRepository<TE> where TE:Entity
    {
        private readonly AccountEntity _account;
        private readonly IGenericRepository<TE> _dbRepo;
        private readonly AccessHelper _accessHelper;
        
        public AccessRepository(AccountEntity account, IGenericRepository<TE> dbRepo )
        {
            _account = account;
            _dbRepo = dbRepo;
            _accessHelper = new AccessHelper();
        }
        public async Task<Entity> GetOneEntityBySelector(string selector)
        {
            return await _dbRepo.GetOneEntityBySelector(selector);
        }

        public async Task<IEnumerable<TE>> All()
        {
            var all = await _dbRepo.All();
            var all2 = _accessHelper.GetEntitiesAccountHasAccess(_account, all);
            return all2;
        }

        public async Task<TE> GetOneBySelector(string selector)
        {
            var e = await _dbRepo.GetOneBySelector(selector);
            if (!_accessHelper.AccountHasAccessTo(_account, e))
            {
                throw new UnauthorizedAccessException();
            }
            return e;
        }

        public async Task<TE> GetOne(Func<TE, bool> condition)
        {
            var e = await _dbRepo.GetOne(condition);
            if (!_accessHelper.AccountHasAccessTo(_account, e))
            {
                throw new UnauthorizedAccessException();
            }
            return e;
        }

        public async Task<TE> Add(TE entity)
        {
            return await _dbRepo.Add(entity);
        }

        public async Task<IEnumerable<TE>> Add(params TE[] entities)
        {
            return await _dbRepo.Add(entities);
        }

        public async Task<int> CountAsync(Func<TE, bool> condition)
        {
            Func<TE,bool> cond2 = (TE e) => _accessHelper.AccountHasAccessTo(_account, e) && condition(e);
            return await _dbRepo.CountAsync(cond2);
        }

        public int Count(Func<TE, bool> condition = null)
        {
            Func<TE, bool> cond2 =
                e => _accessHelper.AccountHasAccessTo(_account, e) && (condition == null || condition(e));
            return _dbRepo.Count(condition);
        }

        public async Task<TE> Update(string selector, TE entity)
        {
            if (!_accessHelper.AccountHasAccessTo(_account, selector, _dbRepo))
            {
                throw new UnauthorizedAccessException();
            }
            return await _dbRepo.Update(selector, entity);
        }

        public async Task<Entity> GetOneByType(string selector, Type type)
        {
            var e = await _dbRepo.GetOneByType(selector, type);
            if (!_accessHelper.AccountHasAccessTo(_account, e))
            {
                throw new UnauthorizedAccessException();
            }
            return e;
        }

        public void Attach(TE entity)
        {
            _dbRepo.Attach(entity);
        }

        public void SetModifiedProperty(TE entity, string propertyName)
        {
            _dbRepo.SetModifiedProperty(entity, propertyName);
        }

        public async Task SaveChangesAsync()
        {
            await _dbRepo.SaveChangesAsync();
        }
    }
}