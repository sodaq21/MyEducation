using ASPNET_Practice.DTOs;
using ASPNET_Practice.Models;
using ASPNET_Practice.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarResponse>>> GetAll([FromQuery] CarBodyType? carBodyType)
        {
            var cars = await _carService.GetCarsAsync(carBodyType);
            return Ok(cars.Select(CarResponse.FromCar));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarResponse>> GetById([FromRoute] int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car is null)
            {
                return NotFound();
            }
            return Ok(CarResponse.FromCar(car));
        }

        [HttpPost]
        public async Task<ActionResult<CarResponse>> Create(CreateCarRequest request)
        {
            var car = await _carService.CreateAsync(request.Brand, request.Model, request.ProductionDate, request.HorsePower, request.BodyType);
            return CreatedAtAction(nameof(GetById), new { id = car.id }, CarResponse.FromCar(car));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool res = await _carService.DeleteAsync(id);
            if (!res)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id:int}/horsePower")]
        public async Task<ActionResult<CarResponse>> UpdateHorsePower(int id, UpdateHorsePowerRequest request)
        {
            var car = await _carService.UpdateHorsePowerAsync(id, request.horsePower);
            if (car is null)
                return NotFound();
            return Ok(CarResponse.FromCar(car));
        }
    }
}
