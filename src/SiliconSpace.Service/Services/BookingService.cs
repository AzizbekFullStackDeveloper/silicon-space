using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Booking;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<CoworkingZone> _coworkingZoneRepository;
        private readonly IMapper _mapper;
        public BookingService()
        {
            
        }

        public BookingService(IRepository<Booking> repository, IMapper mapper, IRepository<CoworkingZone> coworkingZoneRepository, IRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _coworkingZoneRepository = coworkingZoneRepository;
            _userRepository = userRepository;
        }

        public async Task<BookingForResultDto> CreateAsync(BookingForCreationDto dto)
        {
            var CheckUser = await this._userRepository.SelectAll().Where(e => e.Id == dto.UserId && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (CheckUser == null)
            {
                throw new SiliconSpaceException(404, "Foydalanuvchi mavjud emas");
            }

            var CheckCoworkingZone = await this._coworkingZoneRepository.SelectAll().Where(e => e.Id == dto.CoworkingZoneId && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (CheckCoworkingZone == null)
            {
                throw new SiliconSpaceException(404, "Coworking joyi mavjud emas");
            }

            var NewBookingCreation = new Booking()
            {
                StatusId = 3,
                CoworkingZoneId = dto.CoworkingZoneId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
            };

            var Result = await this._repository.AddAsync(NewBookingCreation);

            return this._mapper.Map<BookingForResultDto>(Result);
        }

        public async Task<IEnumerable<BookingForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var AllBookings = await this._repository.SelectAll().Where(e => e.StatusId != 2).Include(e => e.CoworkingZone).Include(e => e.User).AsNoTracking().ToListAsync();
            if (AllBookings != null)
            {
                return this._mapper.Map<IEnumerable<BookingForResultDto>>(AllBookings);
            }
            throw new SiliconSpaceException(404, "Ma'lumotlar mavjud emas");
        }

        public async Task<BookingForResultDto> GetByIdAsync(Guid Id)
        {
            var AllBookings = await this._repository.SelectAll().Where(e => e.StatusId != 2 && e.Id == Id).Include(e => e.CoworkingZone).Include(e => e.User).AsNoTracking().FirstOrDefaultAsync();
            if (AllBookings != null)
            {
                return this._mapper.Map<BookingForResultDto>(AllBookings);
            }
            throw new SiliconSpaceException(404, "Ma'lumotlar mavjud emas");
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var AllBookings = await this._repository.SelectAll().Where(e => e.StatusId != 2 && e.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            if (AllBookings != null)
            {
                return await this._repository.DeleteAsync(Id);
            }
            throw new SiliconSpaceException(404, "Ma'lumotlar mavjud emas");
        }

        public async Task<BookingForResultDto> UpdateAsync(Guid Id, BookingForUpdateDto dto)
        {
            var BookingData = await this._repository.SelectAll().Where(e => e.Id == Id && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();

            if (BookingData != null)
            {
                BookingData.StatusId = dto.StatusId != 0 ? dto.StatusId : BookingData.StatusId;
                BookingData.CreatedAt = DateTime.UtcNow;
                var UpdatedResult = await this._repository.UpdateAsync(BookingData);
                return this._mapper.Map<BookingForResultDto>(UpdatedResult);
            }

            throw new SiliconSpaceException(404, "Ma'lumotlar mavjud emas");

        }
    }
}
