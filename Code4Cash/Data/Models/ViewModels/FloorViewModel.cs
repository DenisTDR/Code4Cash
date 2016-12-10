using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels
{
    public class FloorViewModel:ViewModel
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public List<MeetingRoomViewModel> MeetingRooms { get; set; }
    }
}