using System;
using System.Collections.Generic;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;

namespace Code4Cash.Data.Models.Entities
{
    public class BookingEntity:Entity
    {
        public virtual MeetingRoomEntity MeetingRoom { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual AccountEntity Owner { get; set; }
        public virtual IList<MeetingRoomPropertyUpdateLogEntity> PropertyUpdates { get; set; }
    }
}