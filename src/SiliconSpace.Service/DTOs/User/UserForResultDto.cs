using SiliconSpace.Service.DTOs.Booking;

namespace SiliconSpace.Service.DTOs.User
{
    public class UserForResultDto
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<BookingForResultDto> Bookings { get; set; }
    }
}
