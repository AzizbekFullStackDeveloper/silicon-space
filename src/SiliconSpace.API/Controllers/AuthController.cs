using Microsoft.AspNetCore.Mvc;
using SiliconSpace.Service.DTOs.Login;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> PostAsync(LoginDto dto)
        {
            var token = await this._authService.AuthenticateAsync(dto);
            return Ok(token);
        }

    }
}
