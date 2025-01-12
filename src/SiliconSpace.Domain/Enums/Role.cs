using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SiliconSpace.Domain.Entities;

namespace SiliconSpace.Domain.Enums
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
