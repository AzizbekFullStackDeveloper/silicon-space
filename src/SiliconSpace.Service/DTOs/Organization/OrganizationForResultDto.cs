using SiliconSpace.Service.DTOs.Coworking;

namespace SiliconSpace.Service.DTOs.Organization
{
    public class OrganizationForResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public virtual ICollection<CoworkingForResultDto> Coworkings { get; set; }
    }
}
