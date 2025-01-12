using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.Booking
{
    public class BookingForCreationDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CoworkingZoneId { get; set; }
    }
}
