using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Coworking;
using SiliconSpace.Service.Extensions;
using SiliconSpace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiliconSpace.Service.Services
{
    public class CoworkingService : ICoworkingService
    {
        private readonly IRepository<Coworking> _repository;
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;

        public CoworkingService(IRepository<Coworking> repository, IMapper mapper, IRepository<Organization> organizationRepository, IFileUploadService fileUploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _organizationRepository = organizationRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<CoworkingForResultDto> CreateAsync(CoworkingForCreationDto dto)
        {
            var organization = await _organizationRepository.SelectAll().Where(o => o.Id == dto.OrganizationId && o.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (organization == null)
            {
                throw new Exception("Tashkilot mavjud emas");
            }

            var coworking = _mapper.Map<Coworking>(dto);
            coworking.StatusId = 1;
            coworking.CreatedAt = DateTime.UtcNow;

            if (dto.Image != null)
            {
                var ImagePath = await this._fileUploadService.FileUploadAsync(dto.Image, "CoworkingImages");
                coworking.Image = ImagePath;
            }
            var createdCoworking = await _repository.AddAsync(coworking);

            return _mapper.Map<CoworkingForResultDto>(createdCoworking);
        }

        public async Task<IEnumerable<CoworkingForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var coworkings = await _repository.SelectAll().Where(e => e.StatusId != 2).Include(e => e.Organization).ToPagedList(@params).ToListAsync();
            if (coworkings == null)
            {
                throw new Exception("Coworking markazlar mavjud emas");
            }
            return _mapper.Map<IEnumerable<CoworkingForResultDto>>(coworkings);
        }

        public async Task<CoworkingForResultDto> GetByIdAsync(Guid Id)
        {
            var coworking = await _repository.SelectAll().Where(c => c.Id == Id && c.StatusId != 2).Include(e => e.Organization).AsNoTracking().FirstOrDefaultAsync();

            if (coworking == null)
            {
                throw new Exception("Coworking markaz mavjud emas");
            }

            return _mapper.Map<CoworkingForResultDto>(coworking);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var coworking = await _repository.SelectAll().FirstOrDefaultAsync(c => c.Id == Id && c.StatusId != 2);

            if (coworking == null)
            {
                throw new Exception("Coworking markaz mavjud emas");
            }
            if(coworking.Image != null)
            {
                await this._fileUploadService.FileDeleteAsync(coworking.Image);
            }
            return await _repository.DeleteAsync(Id);
        }

        public async Task<CoworkingForResultDto> UpdateAsync(Guid Id, CoworkingForUpdateDto dto)
        {
            var coworking = await _repository.SelectAll().Where(c => c.Id == Id && c.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();

            if (coworking == null)
            {
                throw new Exception("Coworking markaz mavjud emas");
            }
            if (dto.Image != null)
            {
                if (coworking.Image != null)
                {
                    await this._fileUploadService.FileDeleteAsync(coworking.Image);
                }
                var ImagePath = await this._fileUploadService.FileUploadAsync(dto.Image, "CoworkingImages");
                coworking.Image = ImagePath;

            }

            coworking.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : coworking.Title;
            coworking.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : coworking.Description;
            coworking.OpeningHours = !string.IsNullOrEmpty(dto.OpeningHours) ? dto.OpeningHours : coworking.OpeningHours;
            coworking.UpdatedAt = DateTime.UtcNow;

            var updatedCoworking = await _repository.UpdateAsync(coworking);
            
            return _mapper.Map<CoworkingForResultDto>(updatedCoworking);

        }
    }
}
