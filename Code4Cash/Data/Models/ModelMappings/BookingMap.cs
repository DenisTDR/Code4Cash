using Code4Cash.Data.Models.Entities;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels;

namespace Code4Cash.Data.Models.ModelMappings
{
    public class BookingMap : EntityViewModelMap<BookingEntity, BookingViewModel>
    {
        public BookingMap()
        {
            HasRequired(booking => booking.MeetingRoom).WithMany(room => room.Bookings);
        }
    }
}
