using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Commons;

namespace SiliconSpace.Domain.Entities
{
    public class Coworking : Auditable
    {
        public Guid OrganizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string OpeningHours { get; set; }
        public virtual ICollection<CoworkingZone> CoworkingZones { get; set; } = new List<CoworkingZone>();
        public virtual Organization Organization { get; set; }
    }
}
