using System.Collections.Generic;
using Code4Cash.Data.Models.Entities.Base;

namespace Code4Cash.Data.Models.Entities
{
    public class FloorEntity : Entity
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public virtual IList<MeetingRoomEntity> MeetingRooms { get; set; } = new List<MeetingRoomEntity>();
    }
}