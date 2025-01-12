using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconSpace.API.Models;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Coworking;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.API.Controllers
{
    public class CoworkingController : BaseController
    {
        private readonly ICoworkingService _coworkingService;

        public CoworkingController(ICoworkingService CoworkingService)
        {
            _coworkingService = CoworkingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoworkingAsync([FromForm] CoworkingForCreationDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCoworkingsAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingService.GetAllAsync(@params)
            };
            response.MapPaginationHeader();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoworkingById([FromRoute(Name = "id")] Guid Id)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid Id, [FromForm] CoworkingForUpdateDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingService.UpdateAsync(Id, dto)
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
                Data = await this._coworkingService.RemoveAsync(Id)
            };
            return Ok(response);
        }
    }
}
