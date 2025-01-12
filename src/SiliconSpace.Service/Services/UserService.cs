using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.User;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Extensions;
using SiliconSpace.Service.Helpers;
using SiliconSpace.Service.Interfaces;
using ZiggyCreatures.Caching.Fusion;

namespace SiliconSpace.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;
        private readonly IRepository<User> _repository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IOrganizationService _organizationService;
        private IFusionCache _cache;
        public UserService(IMapper mapper, IFileUploadService fileUploadService, IRepository<User> repository, IGuidGenerator guidGenerator, IOrganizationService organizationService, IFusionCache cache)
        {
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _repository = repository;
            _guidGenerator = guidGenerator;
            _organizationService = organizationService;
            _cache = cache;
        }

        public async Task<UserForResultDto> CreateAsync(UserForCreationDto dto)
        {
            var guidGenerator1 = _guidGenerator.GenerateGuid();
            var guidGenerator2 = _guidGenerator.GenerateGuid();

            await _organizationService.GetAllAsync(new PaginationParams());

            var CheckUser = await _repository.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
            if (CheckUser != null)
            {
                throw new SiliconSpaceException(404, "Foydalanuvchi mavjud emas");
            }

            var user = _mapper.Map<User>(dto);
            var GeneratedPassword = PasswordHelper.Hash(dto.Password);
            user.Salt = GeneratedPassword.Salt;
            user.Password = GeneratedPassword.Hash;
            user.StatusId = 1;
            user.RoleId = 1;
            user.CreatedAt = DateTime.UtcNow;
            if (dto.Image != null)
            {
                var ImagePath = await _fileUploadService.FileUploadAsync(dto.Image, "UserImages");
                user.Image = ImagePath;
            }
            var createdUser = await _repository.AddAsync(user);

            return _mapper.Map<UserForResultDto>(createdUser);
        }

        public async Task<IEnumerable<UserForResultDto>> GetAllAsync(PaginationParams @params)
        {
            var cachedUsers = await _cache.GetOrSetAsync<IEnumerable<User>>(
                $"users:{@params.PageIndex}:{@params.PageSize}",
                async (cancellationToken) =>
                {
                    return await _repository.SelectAll()
                        .Where(u => u.StatusId != 2)
                        .ToPagedList(@params)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
                }
            );

            return _mapper.Map<IEnumerable<UserForResultDto>>(cachedUsers);
        }


        public async Task<UserForResultDto> GetByIdAsync(Guid Id)
        {
            var user = await _repository.SelectAll().Where(u => u.Id == Id && u.StatusId != 2).Include(e => e.Bookings).AsNoTracking().FirstOrDefaultAsync();

            if (user == null)
            {
                throw new SiliconSpaceException(404, "Foydalanuvchi mavjud emas");
            }
            return _mapper.Map<UserForResultDto>(user);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var user = await _repository.SelectAll().Where(u => u.Id == Id && u.StatusId != 2).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new SiliconSpaceException(404, "Foydalanuvchi mavjud emas");
            }
            if (user.Image != null)
            {
                await _fileUploadService.FileDeleteAsync(user.Image);
            }
            return await _repository.DeleteAsync(Id);
        }

        public async Task<UserForResultDto> UpdateAsync(Guid Id, UserForUpdateDto dto)
        {
            var user = await _repository.SelectAll().Where(u => u.Id == Id && u.StatusId != 2).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new SiliconSpaceException(404, "Foydalanuvchi mavjud emas");
            }

            user.Firstname = !string.IsNullOrEmpty(dto.Firstname) ? dto.Firstname : user.Firstname;
            user.Lastname = !string.IsNullOrEmpty(dto.Lastname) ? dto.Lastname : user.Lastname;
            if (dto.Image != null)
            {
                await _fileUploadService.FileDeleteAsync(user.Image);
                var ImagePath = await _fileUploadService.FileUploadAsync(dto.Image, "UserImages");
                user.Image = ImagePath;
            }
            user.UpdatedAt = DateTime.UtcNow;
            var updatedUser = await _repository.UpdateAsync(user);

            return _mapper.Map<UserForResultDto>(updatedUser);
        }
    }
}
