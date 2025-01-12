using SiliconSpace.Service.DTOs.Booking;
using SiliconSpace.Service.DTOs.Coworking;

namespace SiliconSpace.Service.DTOs.CoworkingZone
{
    public class CoworkingZoneForResultDto
    {
        public Guid Id { get; set; }
        public Guid CoworkingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string OpeningHours { get; set; }
        public decimal Cost { get; set; }
        public virtual ICollection<BookingForResultDto> Bookings { get; set; } 
        public virtual CoworkingForResultDto Coworking { get; set; }
    }
}
