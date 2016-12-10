using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities
{
    public class MeetingRoomPropertyUpdateLogEntity:Entity
    {
        public virtual MeetingRoomEntity MeetingRoom { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public int UpdateId { get; set; }
    }
}