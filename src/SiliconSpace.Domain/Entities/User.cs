using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Enums;
using SiliconSpace.Domain.Commons;

namespace SiliconSpace.Domain.Entities
{
    public class User : Auditable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; } = 1;
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual Role Role { get; set; }
        public virtual Status Status { get; set; }
    }
}
