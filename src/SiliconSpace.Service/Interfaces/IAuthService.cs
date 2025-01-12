using SiliconSpace.Service.DTOs.Login;

namespace SiliconSpace.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginForResultDto> AuthenticateAsync(LoginDto dto);
    }
}
