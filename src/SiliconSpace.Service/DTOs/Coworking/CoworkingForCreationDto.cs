using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.Coworking
{
    public class CoworkingForCreationDto
    {
        [Required]
        public Guid OrganizationId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string OpeningHours { get; set; }
    }
}
