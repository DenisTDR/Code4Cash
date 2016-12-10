using System.Threading.Tasks;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Base
{
    public interface IRepository
    {
        Task<Entity> GetOneEntityBySelector(string selector);
    }
}