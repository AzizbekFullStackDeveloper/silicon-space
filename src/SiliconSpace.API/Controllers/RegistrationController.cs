using Microsoft.AspNetCore.Mvc;
using SiliconSpace.API.Models;
using SiliconSpace.Service.DTOs.Registration;
using SiliconSpace.Service.DTOs.SMS;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.API.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostAsync([FromBody] RegistrationForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.RegisterUserAsync(dto)
            };
            return Ok(response);
        }

        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCode dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.SendVerificationCodeAsync(dto)
            };
            return Ok(response);
        }
    }
}
