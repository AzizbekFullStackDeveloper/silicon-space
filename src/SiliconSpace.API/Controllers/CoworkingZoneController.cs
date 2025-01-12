using Microsoft.AspNetCore.Mvc;
using SiliconSpace.API.Models;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.CoworkingZone;
using SiliconSpace.Service.Interfaces;

namespace SiliconSpace.API.Controllers
{
    public class CoworkingZoneController : BaseController
    {
        private readonly ICoworkingZoneService _coworkingZoneService;

        public CoworkingZoneController(ICoworkingZoneService CoworkingZoneService)
        {
            _coworkingZoneService = CoworkingZoneService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoworkingZoneAsync([FromForm] CoworkingZoneForCreationDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingZoneService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCoworkingZonesAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingZoneService.GetAllAsync(@params)
            };
            response.MapPaginationHeader();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoworkingZoneById([FromRoute(Name = "id")] Guid Id)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingZoneService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid Id, [FromForm] CoworkingZoneForUpdateDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._coworkingZoneService.UpdateAsync(Id, dto)
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
                Data = await this._coworkingZoneService.RemoveAsync(Id)
            };
            return Ok(response);
        }
    }
}
