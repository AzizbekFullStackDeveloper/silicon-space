using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.Coworking
{
    public class CoworkingForUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public IFormFile? Image { get; set; }
        public string? OpeningHours { get; set; }
    }
}
