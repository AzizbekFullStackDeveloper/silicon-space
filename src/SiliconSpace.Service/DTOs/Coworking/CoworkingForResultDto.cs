using SiliconSpace.Service.DTOs.Organization;

namespace SiliconSpace.Service.DTOs.Coworking
{
    public class CoworkingForResultDto
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string OpeningHours { get; set; }
        public virtual OrganizationForResultDto Organization { get; set; }
    }
}
