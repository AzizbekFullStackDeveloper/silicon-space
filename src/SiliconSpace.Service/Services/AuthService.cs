using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SiliconSpace.Data.IRepositories;
using SiliconSpace.Domain.Entities;
using SiliconSpace.Service.DTOs.Login;
using SiliconSpace.Service.Exceptions;
using SiliconSpace.Service.Helpers;
using SiliconSpace.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SiliconSpace.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Organization> _organizationRepository;
        public AuthService(IRepository<User> userRepository, IConfiguration configuration, IRepository<Organization> organizationRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _organizationRepository = organizationRepository;
        }

        public async Task<LoginForResultDto> AuthenticateAsync(LoginDto dto)
        {
            // Check if the credentials belong to a user
            var user = await _userRepository.SelectAll()
                .Where(u => u.PhoneNumber == dto.PhoneNumber)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
            {
                // Authenticate user
                var hashedPassword = PasswordHelper.Verify(dto.Password, user.Salt, user.Password);
                if (hashedPassword == false)
                {
                    throw new SiliconSpaceException(400, "Telefon Raqam yoki Parol noto`g`ri");
                }

                return new LoginForResultDto
                {
                    Token = GenerateUserToken(user),
                };
            }

            var CheckOrganization = await _organizationRepository.SelectAll()
                .Where(u => u.PhoneNumber == dto.PhoneNumber)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (CheckOrganization != null)
            {
                // Authenticate user
                var hashedPassword = PasswordHelper.Verify(dto.Password, CheckOrganization.Salt, CheckOrganization.Password);
                if (hashedPassword == false)
                {
                    throw new SiliconSpaceException(400, "Telefon Raqam yoki Parol noto`g`ri");
                }

                return new LoginForResultDto
                {
                    Token = GenerateOrganizationToken(CheckOrganization),
                };
            }


            throw new SiliconSpaceException(400, "Telefon Raqam yoki Parol noto`g`ri");
        }


        private string GenerateUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),  // Use the role passed as an argument
                }),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        private string GenerateOrganizationToken(Organization organization)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", organization.Id.ToString()),
                    new Claim(ClaimTypes.Role, organization.RoleId.ToString()),  // Use the role passed as an argument
                }),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
