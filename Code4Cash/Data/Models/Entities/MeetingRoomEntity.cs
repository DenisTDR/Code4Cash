using System.Collections.Generic;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Models.Entities
{
    public class MeetingRoomEntity : Entity
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public virtual FloorEntity Floor { get; set; }
        public virtual IList<AssetEntity> Assets { get; set; } = new List<AssetEntity>();
        public virtual IList<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
        public virtual IList<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
    }
}