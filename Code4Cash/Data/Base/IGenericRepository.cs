using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Base
{
    public interface IGenericRepository<TE> : IRepository where TE : Entity
    {
        Task<IEnumerable<TE>> All();
        Task<TE> GetOneBySelector(string selector);
        Task<TE> GetOne(Func<TE, bool> condition);
        Task<TE> Add(TE entity);
        Task<IEnumerable<TE>> Add(params TE[] entities);
        Task<int> CountAsync(Func<TE, bool> condition);
        int Count(Func<TE, bool> condition = null);
        Task<TE> Update(string selector, TE entity);
        Task<Entity> GetOneByType(string selector, Type type);
        void Attach(TE entity);
        void SetModifiedProperty(TE entity, string propertyName);
        Task SaveChangesAsync();
    }
}