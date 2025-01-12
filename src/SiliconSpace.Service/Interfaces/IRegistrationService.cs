using SiliconSpace.Service.DTOs.Registration;
using SiliconSpace.Service.DTOs.SMS;
using SiliconSpace.Service.DTOs.User;

namespace SiliconSpace.Service.Interfaces
{
    public interface IRegistrationService
    {
        public Task<UserForResultDto> RegisterUserAsync(RegistrationForCreationDto dto);
        public Task<bool> SendVerificationCodeAsync(SendVerificationCode dto);
    }
}
