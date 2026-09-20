namespace DelegatesPractice
{

    class TemperatureSensor
    {
        public event Action<double> OnThresholdExceed;
        public event Action<double> OnTemperatureChanged;
        private readonly double _threshold;
        private double _temperature;
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnTemperatureChanged?.Invoke(value);
                if (value >= _threshold)
                    OnThresholdExceed?.Invoke(value);
            }
            
        }
        public TemperatureSensor(double threshold)
        {
            _threshold = threshold;
        }
    }

    internal class Program
    {

        static void Logger(double temperature)
        {
            Console.WriteLine($"[LOG]: Temperature: {temperature}");
        }

        static void AlertService(double temperature)
        {
            Console.WriteLine($"[ALERT]: Temperature is high! Value {temperature}");
        }

        static void Main(string[] args)
        {
            TemperatureSensor sensor = new TemperatureSensor(100);
            sensor.OnTemperatureChanged += Logger;
            sensor.OnThresholdExceed += AlertService;
            sensor.Temperature = 20;
            sensor.Temperature = 120;
            sensor.Temperature = 101;
        }
    }
}
