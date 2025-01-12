using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.DTOs.Booking;
using SiliconSpace.Service.DTOs.Coworking;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.CoworkingZone
{
    public class CoworkingZoneForCreationDto
    {
        [Required]
        public Guid CoworkingId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string OpeningHours { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}
