using SiliconSpace.Service.DTOs.CoworkingZone;
using SiliconSpace.Service.DTOs.User;

namespace SiliconSpace.Service.DTOs.Booking
{
    public class BookingForResultDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CoworkingZoneId { get; set; }
        public int StatusId { get; set; } = 1;
        public virtual CoworkingZoneForResultDto CoworkingZone { get; set; }
        public virtual string Status { get; set; }
        public virtual UserForResultDto User { get; set; }
    }
}
