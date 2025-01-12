using SiliconSpace.Domain.Enums;

namespace SiliconSpace.Domain.Commons
{
    public class Auditable
    {
        public Guid Id { get; set; }
        public int StatusId { get; set; } = 1;
        public virtual Status Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
    }
}
