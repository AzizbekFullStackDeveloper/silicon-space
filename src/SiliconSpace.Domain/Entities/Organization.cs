using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Enums;
using SiliconSpace.Domain.Commons;

namespace SiliconSpace.Domain.Entities
{
    public class Organization : Auditable
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public int? RoleId { get; set; }
        public virtual ICollection<Coworking> Coworkings { get; set; } = new List<Coworking>();
    }
}
