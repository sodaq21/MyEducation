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
        public async Task<ActionResult<IEnumerable<CarResponse>>> GetCars([FromQuery] CarBodyType? carBodyType)
        {
            var cars = await _carService.GetCarsAsync(carBodyType);
            return Ok(cars.Select(CarResponse.FromCar));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Car>> GetCarById([FromRoute] int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car is null)
            {
                return NotFound();
            }
            return Ok(CarResponse.FromCar(car));
        }
    }
}
