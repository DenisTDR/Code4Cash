using System;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities
{
    public class BookingEntity:Entity
    {
        public virtual MeetingRoomEntity MeetingRoom { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}