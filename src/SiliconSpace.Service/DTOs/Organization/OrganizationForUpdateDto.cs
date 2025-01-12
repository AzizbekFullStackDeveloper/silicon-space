using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.DTOs.Coworking;

namespace SiliconSpace.Service.DTOs.Organization
{
    public class OrganizationForUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Address { get; set; }
    }
}
