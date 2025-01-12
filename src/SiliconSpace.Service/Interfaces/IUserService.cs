using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.User;

namespace SiliconSpace.Service.Interfaces
{
    public interface IUserService
    {
        public Task<UserForResultDto> CreateAsync(UserForCreationDto dto);
        public Task<UserForResultDto> UpdateAsync(Guid Id, UserForUpdateDto dto);
        public Task<bool> RemoveAsync(Guid Id);
        public Task<UserForResultDto> GetByIdAsync(Guid Id);
        public Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
