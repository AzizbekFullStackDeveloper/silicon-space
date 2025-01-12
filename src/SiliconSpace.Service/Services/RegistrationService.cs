using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.DTOs.Registration;
using SiliconSpace.Service.DTOs.SMS;
using SiliconSpace.Service.DTOs.User;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.Service.Services;

public class RegistrationService : IRegistrationService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserService _userService;
    private readonly ISmsService _smsService;
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;


    public RegistrationService(IRepository<User> userRepository, IMapper mapper, ISmsService smsService, IMemoryCache cache, IUserService userService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _smsService = smsService;
        _cache = cache;
        _userService = userService;
    }

    public async Task<UserForResultDto> RegisterUserAsync(RegistrationForCreationDto dto)
    {
        var CheckWhetherExist = await this._userRepository.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
        if (CheckWhetherExist != null)
        {
            throw new SiliconSpaceException(400, "Bu foydalanuvchi mavjud");
        }
        var PhoneVerification = new CodeVerification()
        {
            PhoneNumber = dto.PhoneNumber,
            VerificationCode = dto.VerificationCode,
        };

        var IsVerified = await VerifyCodeAsync(PhoneVerification);

        if (IsVerified == true)
        {
            _cache.Remove($"{dto.PhoneNumber}_VerificationCode");
            var NewUser = this._mapper.Map<UserForCreationDto>(dto);
            var Result = await this._userService.CreateAsync(NewUser);
            return this._mapper.Map<UserForResultDto>(Result);
        }
        else
        {
            throw new SiliconSpaceException(400, "Verification code is not verified");
        }


    }
    public async Task<bool> SendVerificationCodeAsync(SendVerificationCode dto)
    {

        var CheckWhetherExist = await this._userRepository.SelectAll().Where(e => e.PhoneNumber == dto.PhoneNumber && e.StatusId != 2).AsNoTracking().FirstOrDefaultAsync();
        if (CheckWhetherExist != null)
        {
            throw new SiliconSpaceException(400, "Bu foydalanuvchi mavjud");
        };

        if (CheckWhetherExist == null && !string.IsNullOrEmpty(dto.PhoneNumber))
        {
            var verificationCode = GenerateCodeForPhoneNumberVerificationAsync();
            var cacheKey = $"{dto.PhoneNumber}_VerificationCode";
            var cacheExpiration = TimeSpan.FromSeconds(120); 

            try
            {
                _cache.Set(cacheKey, verificationCode, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheExpiration
                });

                var message = new MessageForCreationDto()
                {
                    PhoneNumber = dto.PhoneNumber,
                    MessageContent = $"azmuz.uz tasdiqlash kodingiz: {verificationCode}"
                };

                await _smsService.SendAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                throw new SiliconSpaceException(500, "Error occurred while sending verification code.");
            }
        }
        else
        {
            throw new SiliconSpaceException(400, "This user already exists.");
        }


    }
    public async Task<bool> VerifyCodeAsync(CodeVerification dto)
    {
        var storedCode = _cache.Get<string>($"{dto.PhoneNumber}_VerificationCode");

        if (storedCode != null && storedCode == dto.VerificationCode)
        {
            return true;
        }

        return false;
    }


    private string GenerateCodeForPhoneNumberVerificationAsync()
    {
        Random rnd = new Random();
        int code = rnd.Next(1000, 10000); // Generates a random 4-digit number
        return code.ToString().Substring(0, 4); // Ensure only the first 4 digits are taken
    }
}

