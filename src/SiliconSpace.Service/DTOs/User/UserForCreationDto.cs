using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.DTOs.Booking;
using System.ComponentModel.DataAnnotations;

namespace SiliconSpace.Service.DTOs.User
{
    public class UserForCreationDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public IFormFile? Image { get; set; }

    }
}
