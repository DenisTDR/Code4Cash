using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Base;
using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Access
{
    public class AccessHelper
    {

        public bool AccountHasAccessTo(AccountEntity account, Entity entity)
        {
            if (entity == null || account == null)
            {
                return false;
            }
            if (account?.Role?.Functions == RoleFunction.All)
            {
                return true;
            }
            var eType = entity.GetType();
            if (typeof(MeetingRoomEntity).IsAssignableFrom(eType))
            {
                var room = (MeetingRoomEntity) entity;
                return room.Accounts.Any(acc => acc.Id == account?.Id);
            }
            if (typeof(BookingEntity).IsAssignableFrom(eType))
            {
                var book = (BookingEntity) entity;
                if (book.Owner.Id == account?.Id)
                {
                    return true;
                }
                if (book.Owner.Role.Power < account?.Role?.Power)
                {
                    return true;
                }
                return AccountHasAccessTo(account, book.MeetingRoom);
            }
            if (typeof(AssetEntity).IsAssignableFrom(eType))
            {
                var asset = (AssetEntity) entity;
                return AccountHasAccessTo(account, asset.Room);
            }

            return true;
        }

        public List<T> GetEntitiesAccountHasAccess<T>(AccountEntity account, IEnumerable<T> entities) where T : Entity
        {
            var list = new List<T>();
            foreach (var entity in entities)
            {
                if (AccountHasAccessTo(account, entity))
                {
                    list.Add(entity);
                }
            }
            return list;
        }

        public bool AccountHasAccessTo<TE>(AccountEntity account, string entitySelector, IGenericRepository<TE> repo)
            where TE : Entity
        {
            var entity = repo.GetOneBySelector(entitySelector).Result;
            return AccountHasAccessTo(account, entity);
        }
    }
}