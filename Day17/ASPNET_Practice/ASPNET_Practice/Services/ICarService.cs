using ASPNET_Practice.Models;

namespace ASPNET_Practice.Services
{
    public interface ICarService
    {
        Task<IReadOnlyList<Car>> GetCarsAsync(CarBodyType? bodyType);
        Task<Car?> GetCarByIdAsync(int id);
        Task<Car> CreateAsync(string brand, string model, DateOnly prodDate, int? hp, CarBodyType? bodyType);
        Task<Car?> UpdateHorsePowerAsync(int id, int hp);
        Task<bool> DeleteAsync(int id);
    }
}
