using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.CoworkingZone;

namespace SiliconSpace.Service.Interfaces
{
    public interface ICoworkingZoneService
    {
        public Task<CoworkingZoneForResultDto> CreateAsync(CoworkingZoneForCreationDto dto);
        public Task<CoworkingZoneForResultDto> UpdateAsync(Guid Id, CoworkingZoneForUpdateDto dto);
        public Task<bool> RemoveAsync(Guid Id);
        public Task<CoworkingZoneForResultDto> GetByIdAsync(Guid Id);
        public Task<IEnumerable<CoworkingZoneForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
