using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Organization;
using SiliconSpace.Service.Helpers;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IRepository<Organization> _repository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;
        private readonly IGuidGenerator _guidGenerator;

        public OrganizationService(IRepository<Organization> repository, IMapper mapper, IFileUploadService fileUploadService, IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _guidGenerator = guidGenerator;
        }

        public async Task<OrganizationForResultDto> CreateAsync(OrganizationForCreationDto dto)
        {
            var existingOrganization = await _repository.SelectAll()
                .Where(o => o.Name == dto.Name && o.StatusId != 2)
                .FirstOrDefaultAsync();

            if (existingOrganization != null)
            {
                throw new Exception("Organization already exists");
            }

            var newOrganization = _mapper.Map<Organization>(dto);
            var GeneratedPassword = PasswordHelper.Hash(newOrganization.Password);
            newOrganization.Salt = GeneratedPassword.Salt;
            newOrganization.Password = GeneratedPassword.Hash;
            newOrganization.CreatedAt = DateTime.UtcNow;
            newOrganization.StatusId = 1;
            newOrganization.RoleId = 4;


            if (dto.Image != null)
            {
                var ImagePath = await _fileUploadService.FileUploadAsync(dto.Image, "OrganizationImages");
                newOrganization.Image = ImagePath;
            }
            var createdOrganization = await _repository.AddAsync(newOrganization);

            return _mapper.Map<OrganizationForResultDto>(createdOrganization);
        }

        public async Task<IEnumerable<OrganizationForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var guid = _guidGenerator.GenerateGuid();
            var organizations = await _repository.SelectAll()
                .Where(o => o.StatusId != 2)
                .Include(e => e.Coworkings)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrganizationForResultDto>>(organizations);
        }

        public async Task<OrganizationForResultDto> GetByIdAsync(Guid Id)
        {
            var organization = await _repository.SelectAll()
                .Where(o => o.Id == Id && o.StatusId != 2)
                .Include(e => e.Coworkings)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            
            if (organization == null)
            {
                throw new Exception("Organization not found or is inactive");
            }

            return _mapper.Map<OrganizationForResultDto>(organization);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var organization = await _repository.SelectAll()
                .FirstOrDefaultAsync(o => o.Id == Id && o.StatusId != 2);

            if (organization == null)
            {
                throw new Exception("Organization not found");
            }
            if (organization.Image != null)
            {
                await _fileUploadService.FileDeleteAsync(organization.Image);
            }
            return await _repository.DeleteAsync(Id);
        }

        public async Task<OrganizationForResultDto> UpdateAsync(Guid Id, OrganizationForUpdateDto dto)
        {
            var organization = await _repository.SelectAll()
                .Where(o => o.Id == Id && o.StatusId != 2)
                .FirstOrDefaultAsync();

            if (organization == null)
            {
                throw new Exception("Organization does not exist or is inactive");
            }
            organization.UpdatedAt = DateTime.UtcNow;
            organization.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : organization.Name;
            organization.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : organization.Description;
            if (dto.Latitude != 0)
            {
                organization.Latitude = dto.Latitude;
            }
            if (dto.Longitude != 0)
            {
                organization.Longitude = dto.Longitude;
            }
            organization.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : organization.Address;

            if (dto.Image != null)
            {
                if (organization.Image != null)
                {
                    await _fileUploadService.FileDeleteAsync(organization.Image);
                }
                var ImagePath = await this._fileUploadService.FileUploadAsync(dto.Image, "OrganizationImages");
                organization.Image = ImagePath;
            }
            var updatedOrganization = await _repository.UpdateAsync(organization);

            return _mapper.Map<OrganizationForResultDto>(updatedOrganization);
        }
    }
}
