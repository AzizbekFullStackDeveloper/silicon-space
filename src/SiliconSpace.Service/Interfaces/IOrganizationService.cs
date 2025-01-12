using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Organization;

namespace SiliconSpace.Service.Interfaces
{
    public interface IOrganizationService
    {
        public Task<OrganizationForResultDto> CreateAsync(OrganizationForCreationDto dto);
        public Task<OrganizationForResultDto> UpdateAsync(Guid Id, OrganizationForUpdateDto dto);
        public Task<bool> RemoveAsync(Guid Id);
        public Task<OrganizationForResultDto> GetByIdAsync(Guid Id);
        public Task<IEnumerable<OrganizationForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
