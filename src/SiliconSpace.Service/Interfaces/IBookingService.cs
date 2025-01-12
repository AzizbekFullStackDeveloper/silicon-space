using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Booking;

namespace SiliconSpace.Service.Interfaces
{
    public interface IBookingService
    {
        public Task<BookingForResultDto> CreateAsync(BookingForCreationDto dto);
        public Task<BookingForResultDto> UpdateAsync(Guid Id, BookingForUpdateDto dto);
        public Task<bool> RemoveAsync(Guid Id);
        public Task<BookingForResultDto> GetByIdAsync(Guid Id);
        public Task<IEnumerable<BookingForResultDto>> GetAllAsync(PaginationParams @params);
    }
}
