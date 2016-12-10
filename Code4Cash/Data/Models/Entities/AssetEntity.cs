using System.Collections.Generic;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Enums;

namespace Code4Cash.Data.Models.Entities
{
    public class AssetEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AssetValue Value { get; set; }
        public virtual MeetingRoomEntity Room { get; set; }
    }
}