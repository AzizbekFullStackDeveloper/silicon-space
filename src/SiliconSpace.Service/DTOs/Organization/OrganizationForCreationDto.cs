using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.DTOs.Coworking;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.Organization
{
    public class OrganizationForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
