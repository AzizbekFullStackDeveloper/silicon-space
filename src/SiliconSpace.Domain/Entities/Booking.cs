using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Enums;
using SiliconSpace.Domain.Commons;

namespace SiliconSpace.Domain.Entities
{
    public class Booking : Auditable
    {
        public Guid UserId { get; set; }
        public Guid CoworkingZoneId { get; set; }
        public virtual CoworkingZone CoworkingZone { get; set; }
        public virtual Status Status { get; set; } 
        public virtual User User { get; set; }
    }
}
