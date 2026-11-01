using ASPNET_Practice.Models;

namespace ASPNET_Practice.Services
{
    public class CarService : ICarService
    {
        private object _lock = new();
        private readonly List<Car> _cars = [];

        public CarService()
        {
            _cars.Add(new Car(1, "Toyota", "Camry", new DateOnly(2022, 5, 15), 203, CarBodyType.Sedan, "1NXBR12E3HXXXXXXX"));
            _cars.Add(new Car(2, "Tesla", "Model Y", new DateOnly(2023, 11, 1), 384, CarBodyType.SUV, "5YJ3E1EBXLFXXXXXX"));
            _cars.Add(new Car(3, "Volkswagen", "Golf", new DateOnly(2019, 3, 22), 150, CarBodyType.Hatchback, "WVWZZZAUZJWXXXXXX"));
            _cars.Add(new Car(4, "Ford", "Mustang", new DateOnly(1967, 4, 17), null, CarBodyType.Coupe, "7R02K1XXXXX"));
            _cars.Add(new Car(5, "Audi", "A6 Avant", new DateOnly(2021, 8, 30), 204, null, "WAUZZZF4LMNXXXXXX"));
        }

        public Task<Car> CreateAsync(string brand, string model, DateOnly prodDate, int? hp, CarBodyType? bodyType)
        {
            lock (_lock)
            {
                throw new NotImplementedException();
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_lock)
            {
                throw new NotImplementedException();
            }
        }

        public Task<Car?> GetCarByIdAsync(int id)
        {
            lock (_lock)
            {
                var car = _cars.FirstOrDefault(c => c.id == id);
                return Task.FromResult<Car?>(car);
            }
        }

        public Task<IReadOnlyList<Car>> GetCarsAsync(CarBodyType? bodyType)
        {
            lock (_lock)
            {
                IEnumerable<Car> query = _cars;
                if (bodyType is not null)
                    query = query.Where(c => c.BodyType == bodyType).ToList();

                var result = query.OrderBy(c => c.id).ToList();
                return Task.FromResult<IReadOnlyList<Car>>(result);
            }
        }

        public Task<Car> UpdateHorsePowerAsync(int id, int hp)
        {
            lock (_lock)
            {
                throw new NotImplementedException();
            }
        }
    }
}
