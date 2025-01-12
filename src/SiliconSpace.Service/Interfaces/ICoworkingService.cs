using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Coworking;

namespace SiliconSpace.Service.Interfaces
{
    public interface ICoworkingService
    {
        public Task<CoworkingForResultDto> CreateAsync(CoworkingForCreationDto dto);
        public Task<CoworkingForResultDto> UpdateAsync(Guid Id, CoworkingForUpdateDto dto);
        public Task<bool> RemoveAsync(Guid Id);
        public Task<CoworkingForResultDto> GetByIdAsync(Guid Id);
        public Task<IEnumerable<CoworkingForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
