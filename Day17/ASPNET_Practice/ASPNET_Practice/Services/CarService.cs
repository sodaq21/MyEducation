using ASPNET_Practice.Models;
using System.Text;

namespace ASPNET_Practice.Services
{
    public class CarService : ICarService
    {
        private object _lock = new();
        private readonly List<Car> _cars = [];
        private int _id = 1;
        private Random _rnd = new Random();

        public CarService()
        {
            _cars.Add(new Car(_id++, "Toyota", "Camry", new DateOnly(2022, 5, 15), 203, CarBodyType.Sedan, "1NXBR12E3HXXXXXXX"));
            _cars.Add(new Car(_id++, "Tesla", "Model Y", new DateOnly(2023, 11, 1), 384, CarBodyType.SUV, "5YJ3E1EBXLFXXXXXX"));
            _cars.Add(new Car(_id++, "Volkswagen", "Golf", new DateOnly(2019, 3, 22), 150, CarBodyType.Hatchback, "WVWZZZAUZJWXXXXXX"));
            _cars.Add(new Car(_id++, "Ford", "Mustang", new DateOnly(1967, 4, 17), null, CarBodyType.Coupe, "7R02K1XXXXX"));
            _cars.Add(new Car(_id++, "Audi", "A6 Avant", new DateOnly(2021, 8, 30), 204, null, "WAUZZZF4LMNXXXXXX"));
        }

        private string VIN_Generator()
        {
            StringBuilder vin = new StringBuilder(17);
            int c;
            for (int i = 1; i <= 17; i++)
            {
                c = _rnd.Next(65, 91);
                if (i == 9 || i is >= 12 and <= 17)
                {
                    vin.Append((char)_rnd.Next(48, 58));
                    continue;
                }
                if (c == 73 || c == 79 || c == 81)
                {
                    i--;
                    continue;
                }

                vin.Append((char)c);
            }

            return vin.ToString();
        }

        public Task<Car> CreateAsync(string brand, string model, DateOnly prodDate, int? hp, CarBodyType? bodyType)
        {
            lock (_lock)
            {
                var car = new Car(_id++, brand, model, prodDate, hp, bodyType, VIN_Generator());
                _cars.Add(car);
                return Task.FromResult(car);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_lock)
            {
                var removed = _cars.RemoveAll(c => c.id == id) > 0;
                return Task.FromResult(removed);
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

        public Task<Car?> UpdateHorsePowerAsync(int id, int hp)
        {
            lock (_lock)
            {
                int index = _cars.FindIndex(c => c.id == id);
                if (index < 0)
                    return Task.FromResult<Car?>(null);

                _cars[index] = _cars[index] with { HorsePower = hp };
                return Task.FromResult<Car?>(_cars[index]);
            }
        }
    }
}
