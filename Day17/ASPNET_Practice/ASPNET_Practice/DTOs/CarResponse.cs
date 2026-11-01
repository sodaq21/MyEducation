using ASPNET_Practice.Models;

namespace ASPNET_Practice.DTOs
{
    public record CarResponse(
        int id,
        string Brand,
        string Model,
        DateOnly ProductionDate,
        int? HorsePower,
        CarBodyType? BodyType)
    {
        public static CarResponse FromCar(Car car)
        {
            return new CarResponse(car.id, car.Brand, car.Model, car.ProductionDate, car.HorsePower, car.BodyType);
        }
    }
}
