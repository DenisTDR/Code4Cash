using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels
{
    public class MeetingRoomPropertyUpdateLogViewModel:ViewModel
    {
        public virtual MeetingRoomViewModel MeetingRoom { get; set; }
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public int UpdateId { get; set; }
    }
}