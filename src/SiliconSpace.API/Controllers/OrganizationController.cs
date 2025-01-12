using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconSpace.API.Models;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Organization;
using SiliconSpace.Service.Interfaces;
using System.Security.Claims;

namespace SiliconSpace.API.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService OrganizationService)
        {
            _organizationService = OrganizationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganizationAsync([FromForm] OrganizationForCreationDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._organizationService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrganizationsAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._organizationService.GetAllAsync(@params)
            };
            response.MapPaginationHeader();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetOrganizationById()
        {
            var UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._organizationService.GetByIdAsync(UserId)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid Id, [FromForm] OrganizationForUpdateDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._organizationService.UpdateAsync(Id, dto)
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
                Data = await this._organizationService.RemoveAsync(Id)
            };
            return Ok(response);
        }
    }
}
