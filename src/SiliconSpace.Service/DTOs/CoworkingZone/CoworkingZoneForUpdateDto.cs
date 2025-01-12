using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.CoworkingZone
{
    public class CoworkingZoneForUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? OpeningHours { get; set; }
        public decimal Cost { get; set; }
    }
}
