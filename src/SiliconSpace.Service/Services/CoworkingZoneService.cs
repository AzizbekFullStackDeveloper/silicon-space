using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.CoworkingZone;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services
{
    public class CoworkingZoneService : ICoworkingZoneService
    {
        private readonly IRepository<CoworkingZone> _repository;
        private readonly IFileUploadService _fileUploadService;
        private readonly IRepository<Coworking> _coworkingRepository;
        private readonly IMapper _mapper;

        public CoworkingZoneService(IRepository<CoworkingZone> repository, IMapper mapper, IRepository<Coworking> coworkingRepository, IFileUploadService fileUploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _coworkingRepository = coworkingRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<CoworkingZoneForResultDto> CreateAsync(CoworkingZoneForCreationDto dto)
        {
            var coworking = await _coworkingRepository.SelectAll().Where(c => c.Id == dto.CoworkingId && c.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (coworking == null)
            {
                throw new Exception("Coworking not found or is inactive");
            }

            var coworkingZone = _mapper.Map<CoworkingZone>(dto);
            coworkingZone.StatusId = 1;
            coworkingZone.CreatedAt = DateTime.UtcNow;
            if (dto.Image != null)
            {
                var ImagePath = await this._fileUploadService.FileUploadAsync(dto.Image, "CoworkingZoneImages");
                coworkingZone.Image = ImagePath;
            }
            var createdCoworkingZone = await _repository.AddAsync(coworkingZone);
            return _mapper.Map<CoworkingZoneForResultDto>(createdCoworkingZone);
        }

        public async Task<IEnumerable<CoworkingZoneForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var coworkingZones = await _repository.SelectAll().Where(e => e.StatusId != 2).Include(e => e.Coworking).ToListAsync();
            return _mapper.Map<IEnumerable<CoworkingZoneForResultDto>>(coworkingZones);
        }

        public async Task<CoworkingZoneForResultDto> GetByIdAsync(Guid Id)
        {
            var coworkingZone = await _repository.SelectAll().Where(e => e.Id == Id && e.StatusId != 2).Include(e => e.Coworking).FirstOrDefaultAsync(c => c.Id == Id);
            if (coworkingZone == null)
            {
                throw new Exception("Coworking zone not found");
            }
            return _mapper.Map<CoworkingZoneForResultDto>(coworkingZone);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var coworkingZone = await _repository.SelectAll().FirstOrDefaultAsync(c => c.Id == Id);
            if (coworkingZone == null)
            {
                throw new Exception("Coworking zone not found");
            }
            if (coworkingZone.Image != null)
            {
                await this._fileUploadService.FileDeleteAsync(coworkingZone.Image);
            }
            return await _repository.DeleteAsync(Id);
        }

        public async Task<CoworkingZoneForResultDto> UpdateAsync(Guid Id, CoworkingZoneForUpdateDto dto)
        {
            var coworkingZone = await _repository.SelectAll().Where(c => c.Id == Id && c.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (coworkingZone == null)
            {
                throw new Exception("Coworking zone not found");
            }

            if (dto.Image != null)
            {
                if (coworkingZone.Image != null)
                {
                    await this._fileUploadService.FileDeleteAsync(coworkingZone.Image);
                }
                var ImagePath = await this._fileUploadService.FileUploadAsync(dto.Image, "CoworkingZoneImages");
                coworkingZone.Image = ImagePath;
            }

            coworkingZone.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : coworkingZone.Title;
            coworkingZone.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : coworkingZone.Description;
            coworkingZone.OpeningHours = !string.IsNullOrEmpty(dto.OpeningHours) ? dto.OpeningHours : coworkingZone.OpeningHours;
            coworkingZone.Cost = dto.Cost != 0 ? dto.Cost : coworkingZone.Cost;
            coworkingZone.UpdatedAt = DateTime.UtcNow;



            var updatedCoworkingZone = await _repository.UpdateAsync(coworkingZone);
            return _mapper.Map<CoworkingZoneForResultDto>(updatedCoworkingZone);
        }
    }
}
