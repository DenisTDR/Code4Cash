using System.Collections.Generic;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Data.Models.ViewModels
{
    public class MeetingRoomViewModel : ViewModel
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public FloorViewModel Floor { get; set; }
        public List<AssetViewModel> Assets { get; set; } = new List<AssetViewModel>();
        public List<BookingViewModel> Bookings { get; set; } = new List<BookingViewModel>();
    }
}