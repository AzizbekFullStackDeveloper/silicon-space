using Microsoft.AspNetCore.Http;
using SiliconSpace.Service.DTOs.Booking;

namespace SiliconSpace.Service.DTOs.User
{
    public class UserForUpdateDto
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? Image { get; set; }
    }
}
