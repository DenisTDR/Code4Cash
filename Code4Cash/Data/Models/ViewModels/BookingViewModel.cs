using System;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels
{
    public class BookingViewModel : ViewModel
    {
        public MeetingRoomViewModel MeetingRoom { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}