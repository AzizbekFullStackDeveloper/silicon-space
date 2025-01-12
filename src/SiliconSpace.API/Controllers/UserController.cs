using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using SiliconSpace.API.Models;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.User;
using SiliconSpace.Service.Interfaces;
using SiliconSpace.Service.Services;
using System.Security.Claims;

namespace SiliconSpace.API.Controllers
{
    public class UserController(IUserService _userService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromForm] UserForCreationDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.GetAllAsync(@params)
            };
            response.MapPaginationHeader();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserById()
        {
            var UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.GetByIdAsync(UserId)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid Id, [FromForm] UserForUpdateDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.UpdateAsync(Id, dto)
            };
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid Id)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpGet("test")]
        public async Task<IActionResult> GetTestResultAsync()
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = "Test data"
            };
            return Ok(response);
        }
    }
}
