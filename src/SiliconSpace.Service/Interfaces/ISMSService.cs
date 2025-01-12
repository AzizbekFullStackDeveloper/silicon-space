using SiliconSpace.Service.DTOs.SMS;

namespace SiliconSpace.Service.Interfaces
{
    public interface ISmsService
    {
        public Task<bool> SendAsync(MessageForCreationDto dto);
    }
}
