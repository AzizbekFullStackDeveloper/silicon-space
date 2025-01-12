using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Enums;
using SiliconSpace.Domain.Commons;

namespace SiliconSpace.Domain.Entities
{
    public class CoworkingZone : Auditable
    {
        public Guid CoworkingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string OpeningHours { get; set; }
        public decimal Cost { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual Coworking Coworking { get; set; }
        public virtual Status Status { get; set; }
    }
}
