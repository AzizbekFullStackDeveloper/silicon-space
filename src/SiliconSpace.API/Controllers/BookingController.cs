using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconSpace.API.Models;
using SiliconSpace.Service.Configurations;
using SiliconSpace.Service.DTOs.Booking;
using SiliconSpace.Service.Interfaces;
using SiliconSpace.Service.Services;
using System.Security.Claims;
using System.Xml.Linq;

namespace SiliconSpace.API.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync([FromForm] BookingForCreationDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._bookingService.CreateAsync(dto)
            };
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookingsAsync([FromQuery] PaginationParams @params)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._bookingService.GetAllAsync(@params)
            };
            response.MapPaginationHeader();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById([FromRoute(Name = "id")] Guid Id)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._bookingService.GetByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] Guid Id, [FromForm] BookingForUpdateDto dto)
        {
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._bookingService.UpdateAsync(Id, dto)
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
                Data = await this._bookingService.RemoveAsync(Id)
            };
            return Ok(response);
        }
    }
}
